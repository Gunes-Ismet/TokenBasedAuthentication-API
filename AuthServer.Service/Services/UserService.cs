using AuthServer.Core.DTO_s;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using AuthServer.Service.AutoMapper;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTO_s;

namespace AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response<UserAppDTO>> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            var user = new UserApp{ Email = createUserDTO.Email, UserName = createUserDTO.UserName };
            var result = await _userManager.CreateAsync(user,createUserDTO.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserAppDTO>.Fail(new ErrorDTO(errors, true), 400);
            }
            return Response<UserAppDTO>.Success(ObjectMapper.Mapper.Map<UserAppDTO>(user), 200);

        }
        public async Task<Response<UserAppDTO>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return Response<UserAppDTO>.Fail("UserName not Found!", 404, true);
            return Response<UserAppDTO>.Success(ObjectMapper.Mapper.Map<UserAppDTO>(user), 200);
        }
    }
}
