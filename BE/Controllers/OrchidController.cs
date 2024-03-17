using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Common.Firebase;
using Services.Common.Jwt;
using Services.Orchid;

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
    }
}
