using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Common.Firebase;
using Services.Common.Jwt;
using Services.Orchid;
using static Services.Orchid.OrchidService;

namespace BE.Controllers
{
    [Route("api/orchid")]
    [ApiController]
    public class OrchidController : ControllerBase
    {
        private readonly IOrchidService _orchidService;
        private readonly IJwtService _jwtService;

        public OrchidController(IOrchidService orchidService, IJwtService jwtService)
        {
            _orchidService = orchidService;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpPost("add-orchid")]
        public async Task<IActionResult> AddOrchid([FromForm] AddOrchidDTO.AddOrchidRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var orchid = await _orchidService.AddOrchid(new AddOrchidDTO.AddOrchidRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Created("null", new AddOrchidDTO.AddOrchidResponse
                {
                    Data = orchid,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }

                });
            }
            catch (OrchidService.AddOrchidException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize]
        [HttpPut("update-orchid")]
        public async Task<IActionResult> UpdateOrchid([FromForm] UpdateOrchidDTO.UpdateOrchidRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var orchid = await _orchidService.UpdateOrchid(new UpdateOrchidDTO.UpdateOrchidRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Ok(new UpdateOrchidDTO.UpdateOrchidResponse
                {
                    Data = orchid,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (OrchidService.UpdateOrchidException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize]
        [HttpDelete("delete-orchid")]
        public async Task<IActionResult> DeleteOrchid(Guid orchidId)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                var orchid = await _orchidService.DeleteOrchid(new DeleteOrchidDTO.DeleteOrchidRequest()
                {
                    UserId = userId.Value,
                    Data = new DeleteOrchidDTO.DeleteOrchidRequestData
                    {
                        OrchidId = orchidId
                    }
                });

                return Ok(new DeleteOrchidDTO.DeleteOrchidResponse
                {
                    Data = orchid,
                    AuthTokens = new AuthTokens
                    {
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (OrchidService.DeleteOrchidException e)
            {
                switch (e.StatusCode)
                {
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [HttpGet("get-orchids-pagination")]
        public async Task<IActionResult> GetOrchidsPagination(int pageSize, int pageNumber)
        {
            try
            {
                var orchids = await _orchidService.GetOrchidsPagination(pageSize, pageNumber);
                return Ok(orchids);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetOrchidById(Guid id)
        {
            try
            {
                var orchids = await _orchidService.GetOrchidById(id);
                return Ok(orchids);
            }
            catch (GetOrchidException e)
            {
                switch (e.StatusCode)
                {
                    case GetOrchidException.StatusCodeEnum.OrchidNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetOrchidByName(string name)
        {
            try
            {
                var orchids = await _orchidService.GetOrchidByName(name);
                return Ok(orchids);
            }
            catch (GetOrchidException e)
            {
                switch (e.StatusCode)
                {
                    case GetOrchidException.StatusCodeEnum.OrchidNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}
