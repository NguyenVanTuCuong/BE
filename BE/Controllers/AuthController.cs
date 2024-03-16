using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Common.Jwt;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IMapper mapper, IJwtService jwtService)
        {
            _authService = authService;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO.SignInRequest data)
        {
            try
            {
                var user = await _authService.SignIn(data);
                var token = _jwtService.GenerateToken(user.UserId, user.Role);

                return Ok(new SignInDTO.SignInResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = token
                    }
                });
            }
            catch (AuthService.SignInException e)
            {
                switch (e.StatusCode)
                {
                    case AuthService.SignInException.StatusCodeEnum.UserNotFound:
                        return NotFound(e.Message);
                    case AuthService.SignInException.StatusCodeEnum.PasswordNotMatch:
                        return BadRequest(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}
