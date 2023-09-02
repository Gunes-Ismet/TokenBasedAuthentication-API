using AuthServer.Core.DTO_s;
using SharedLibrary.DTO_s;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDTO>> CreateToken(LoginDTO loginDTO);
        Task<Response<TokenDTO>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoDataDTO>> RevokeRefreshToken(string refreshToken);
        Response<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO);
    }
}
