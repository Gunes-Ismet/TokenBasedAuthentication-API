using AuthServer.Core.DTO_s;
using SharedLibrary.DTO_s;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO);
        Task<Response<TokenDTO>> CreateTokenByRefreshTokenAsync(RefreshTokenDTO refreshToken);
        Task<Response<NoDataDTO>> RevokeRefreshToken(RefreshTokenDTO refreshToken);
        Response<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO);
    }
}
