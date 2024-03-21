using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using BussinessObjects.Models;
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

        //Add orchid from dto
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddOrchid([FromForm] AddOrchidDTO.AddOrchidRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                //add with userId and all data from orchid request
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
        [HttpPut]
        public async Task<IActionResult> UpdateOrchid([FromForm] UpdateOrchidDTO.UpdateOrchidRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                //update with data and user id
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
            catch (UpdateOrchidException e)
            {
                switch (e.StatusCode)
                {
                    case UpdateOrchidException.StatusCodeEnum.OrchidNotFound:
                        return NotFound(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        //Delete orchid from dto
        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeleteOrchid(Guid orchidId)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {
                //delete orchid with id
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

        [HttpGet]
        public async Task<IActionResult> GetOrchidsPagination(int skip, int top)
        {
            try
            {
                //get list orchid with pageSize and pageNumber
                var orchids = await _orchidService.GetOrchidsPagination(skip, top);
                return Ok(new GetOrchidDTO.GetOrchidListResponse
                {
                    orchids = orchids.orchids,
                    pages = orchids.pages
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrchidById(Guid id)
        {
            try
            {
                //get orchid by id
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
        [Route("search")]
        public async Task<IActionResult> SearchOrchids(string? name, string? decription, DepositStatus? depositStatus, int skip, int top)
        {
            try
            {
                //search with name, description, depositStatus, pageNumber and pageSize
                var orchids = await _orchidService.SearchOrchids(name, decription, depositStatus, skip, top);

                return Ok(new GetOrchidDTO.GetOrchidListResponse
                {
                    orchids = orchids.orchids,
                    pages = orchids.pages
                }) ;
            }
            catch (OrchidService.GetOrchidException e)
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
