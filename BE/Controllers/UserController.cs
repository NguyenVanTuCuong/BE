using Azure;
using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.User;
using static Services.User.UserService;

namespace BE.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPagination(int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);

            try
            {
                var response = await _userService.GetAllPagination(skip, top);


                return Ok(new GetUserListResponse()
                {
                    Data = response,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);

            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(new DetailsUserDTO.DetailsUserResponse()
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (UserService.UserException e)
            {
                switch (e.StatusCode)
                {
                    case UserException.StatusCodeEnum.UserNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [HttpGet]
        [Route("search/{input}")]
        public async Task<IActionResult> SearchUserByNameOrEmail(string input, int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);

            try
            {
                var response = await _userService.SearchWithPagination(skip, top, input);

                return Ok(new GetUserListResponse()
                {
                    Data = response,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO.AddUserRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var user = await _userService.AddAsync(new AddUserDTO.AddUserRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Created("null", new AddUserDTO.AddUserResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }

                });
            }
            catch (UserService.UserException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO.UpdateUserRequestData data, Guid id)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var user = await _userService.UpdateAsync(new UpdateUserDTO.UpdateUserRequest()
                {
                    UserId = id,
                    Data = data
                }, id);

                return Ok(new UpdateUserDTO.UpdateUserResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (UserService.UserException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var user = await _userService.DeleteAsync(new DeleteUserDTO.DeleteUserRequest()
                {
                    UserId = userId.Value,
                    Data = new DeleteUserDTO.DeleteUserRequestData
                    {
                        UserId = id
                    }
                });

                return Ok(new DeleteUserDTO.DeleteUserResponse
                {
                    Data = user,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (UserService.UserException e)
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