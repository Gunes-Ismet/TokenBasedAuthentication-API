using AuthServer.Core.Configuration;
using AuthServer.Core.DTO_s;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTO_s;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;
        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService,UserManager<UserApp> userManager, IUnitOfWork unitOfWork,IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }
        public async Task<Response<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO)
        {
            if (loginDTO == null) 
                throw new ArgumentNullException(nameof(loginDTO));
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) 
                return Response<TokenDTO>.Fail("Email or Password is wrong", 400, true);
            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password)) 
                return Response<TokenDTO>.Fail("Email or Password is wrong", 400, true);
            var token = await _tokenService.CreateToken(user);
            var userRefreshToken = _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefault();
            if (userRefreshToken == null)
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return Response<TokenDTO>.Success(token, 200);
        }
        public Response<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDTO.ClientId && x.Secret == clientLoginDTO.ClientSecret);
            if (client == null) 
                return Response<ClientTokenDTO>.Fail("ClientId or ClientSecret not Found!",404,true);
            var token = _tokenService.CreateClientToken(client);
            return Response<ClientTokenDTO>.Success(token,200);
        }
        public async Task<Response<TokenDTO>> CreateTokenByRefreshTokenAsync(RefreshTokenDTO refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken.RefreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
                return Response<TokenDTO>.Fail("RefreshToken not Found!", 404, true);
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
                return Response<TokenDTO>.Fail("UserId not Found!",404,true);
            var tokenDto = await _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return Response<TokenDTO>.Success(tokenDto,200);
        }
        public async Task<Response<NoDataDTO>> RevokeRefreshToken(RefreshTokenDTO refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken.RefreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
                return Response<NoDataDTO>.Fail("RefreshToken not Found!", 404, true);
            _userRefreshTokenService.Remove(existRefreshToken);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTO>.Success(200);
        }
    }
}
