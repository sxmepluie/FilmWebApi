using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AuthController(IServiceManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoForInsertion userDto)
        {
            var result = await _manager.AuthenticationService.CreateUser(userDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var isValidUser = await _manager.AuthenticationService.ValidateUser(user);
            if (!isValidUser)
                return Unauthorized();

            var tokenDto = await _manager.AuthenticationService.CreateToken(true);
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoForReturn = await _manager.AuthenticationService.RefreshToken(tokenDto);
            if (tokenDtoForReturn == null)
                return BadRequest();

            return Ok(tokenDtoForReturn);
        }
    }
}