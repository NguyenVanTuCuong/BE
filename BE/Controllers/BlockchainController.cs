using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Blockchain;
using Services.Common.Jwt;
using Services.Orchid;

namespace BE.Controllers
{
    [Route("api/blockchain")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;
        private readonly IJwtService _jwtService;

        public BlockchainController(IBlockchainService blockchainService, IJwtService jwtService)
        {
            _blockchainService = blockchainService;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpPost("deposit-for-nft")]
        public async Task<IActionResult> DepositForNft([FromForm] DepositForNftDTO.DepositForNftRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var orchid = await _blockchainService.DepositForNft(new DepositForNftDTO.DepositForNftRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Created("null", new DepositForNftDTO.DepositForNftResponse
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
