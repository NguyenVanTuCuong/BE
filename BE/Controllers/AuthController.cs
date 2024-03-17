using AutoMapper;
using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Common.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO.SignInRequest data)
        {
            try
            {
                var user = await _authService.SignIn(data);

                return Ok(new SignInDTO.SignInResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(user.UserId)
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

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO.SignUpRequest data)
        {
            try
            {
                var user = await _authService.SignUp(data);

                return Created("null", new SignUpDTO.SignUpResponse()
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                      AccessToken = await _jwtService.GenerateToken(user.UserId),           
                    }
                });
            }
            catch (AuthService.SignUpException e)
            {
                switch (e.StatusCode)
                {
                    case AuthService.SignUpException.StatusCodeEnum.UserEmailExisted:
                        return Conflict(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize]
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);

            try
            {
                var user = await _authService.GetProfile(new GetProfileDTO.GetProfileRequest()
                {
                    UserId = userId.Value
                });

                return Ok(new GetProfileDTO.GetProfileResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(user.UserId)
                    }
                });
            }
            catch (AuthService.SignInException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}
