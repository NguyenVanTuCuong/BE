using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Blockchain;
using Services.Common.Jwt;
using Services.Orchid;
using static Services.Blockchain.BlockchainService;

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

        [Authorize(Roles = "Administrator")]
        [HttpPost("deposit-for-nft")]
        public async Task<IActionResult> DepositForNft([FromBody] DepositForNftDTO.DepositForNftRequestData data)
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
            catch (DepositForNftException e)
            {
                switch (e.StatusCode)
                {
                    case DepositForNftException.StatusCodeEnum.AlreadyDeposited:
                        return Conflict(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }

        [Authorize]
        [HttpPost("withdraw-nft")]
        public async Task<IActionResult> WithdrawNft([FromBody] WithdawNftDTO.WithdawNftRequestData data)
        {
            var userId = _jwtService.GetUserIdFromContext(HttpContext);
            try
            {

                var response = await _blockchainService.WithdrawNft(new WithdawNftDTO.WithdawNftRequest()
                {
                    UserId = userId.Value,
                    Data = data
                });

                return Ok(new WithdawNftDTO.WithdawNftResponse
                {
                    Data = response,
                    AuthTokens = new AuthTokens
                    {   
                        AccessToken = await _jwtService.GenerateToken(userId.Value),
                    }
                });
            }
            catch (WithdrawNftException e)
            {
                switch (e.StatusCode)
                {
                    case WithdrawNftException.StatusCodeEnum.NotOwned:
                        return Conflict(e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}
