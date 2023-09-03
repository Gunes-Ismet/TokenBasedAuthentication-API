using AuthServer.Core.Configuration;
using AuthServer.Core.DTO_s;
using AuthServer.Core.Entities;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        Task<TokenDTO> CreateToken(UserApp userApp);
        ClientTokenDTO CreateClientToken(Client client);
    }
}
