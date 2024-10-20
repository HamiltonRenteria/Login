using Application.DTOs.User.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public LoginController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Generate/Token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return Ok(response);
        }

    }
}
