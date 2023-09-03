using AuthServer.Core.DTO_s;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDTO model)
        {
            var result = await _authenticationService.CreateTokenAsync(model);
            return ActionResultInstance(result);            
        }
        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDTO model)
        {
            var result =  _authenticationService.CreateTokenByClient(model);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTO model)
        {
            var result = await _authenticationService.RevokeRefreshToken(model);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTO model)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(model);
            return ActionResultInstance(result);
        }
    }
}
