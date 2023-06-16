using Business.BServices;
using Core.CEntities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDto loginDto)
        {
            var requestUrl = loginDto.HostName;
            var userToLogin = _authService.Login(loginDto, requestUrl);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            return Ok(userToLogin.Data);
        }
    }
}
