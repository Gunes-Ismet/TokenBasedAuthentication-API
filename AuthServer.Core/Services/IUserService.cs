using AuthServer.Core.DTO_s;
using SharedLibrary.DTO_s;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDTO>> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<Response<UserAppDTO>> GetUserByNameAsync(string userName);
    }
}
