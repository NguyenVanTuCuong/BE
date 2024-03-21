using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Jwt;
using Services.DepositRequest;
using Services.Orchid;
using static Services.DepositRequest.DepositRequestService;

namespace BE.Controllers
{
    [Route("api/deposit-request")]
    [ApiController]
    public class DepositRequestController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IDepositRequestService _depositRequestService;
        public DepositRequestController(IJwtService jwtService, IDepositRequestService depositRequestService)
        {
            _jwtService = jwtService;
            _depositRequestService = depositRequestService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddDepositRequest([FromBody] AddDepositRequestDTO.AddDepositRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var depositRequest = await _depositRequestService.AddDepositRequest(new AddDepositRequestDTO.AddDepositRequest()
                {
                    UserId = userId.Value,
                    Data = data,
                });

                return Created("null", new AddDepositRequestDTO.AddDepositResponse
                {
                    Data = depositRequest,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }

                });
            }
            catch (DepositRequestService.AddDepositRequestException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateDepositRequest([FromBody] UpdateDepositRequestDTO.UpdateDepositRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var depositRequest = await _depositRequestService.UpdateDepositRequest(new UpdateDepositRequestDTO.UpdateDepositRequest()
                {
                    UserId = userId.Value,
                    Data = data,
                });

                return Ok(new UpdateDepositRequestDTO.UpdateDepositResponse
                {
                    Data = depositRequest,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }

                });
            }
            catch (UpdateDepositRequestException e)
            {
                switch (e.StatusCode)
                {
                    case UpdateDepositRequestException.StatusCodeEnum.DepositRequestNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("/all")]
        public async Task<IActionResult> GetAllDepositRequestPagination(int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            var depositRequest = await _depositRequestService.GetAllDepositRequestPagination(skip, top);
            return Ok(new GetDepositDTO.GetDepositResponse
            {
                Data = depositRequest,
                AuthTokens = new AuthTokens
                {
                    AccessToken = await _jwtService.GenerateToken(userId.Value),
                }
            });
        }

        [Authorize]
        [HttpGet("/curent-user")]
        public async Task<IActionResult> GetDepositRequestByUserIdPagination(int skip, int top)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            var depositRequest = await _depositRequestService.GetDepositRequestByUserIdPagination(userId, skip, top);
            return Ok(new GetDepositDTO.GetDepositResponse
            {
                Data = depositRequest,
                AuthTokens = new AuthTokens
                {
                    AccessToken = await _jwtService.GenerateToken(userId.Value),
                }
            });
        }
    }
}
