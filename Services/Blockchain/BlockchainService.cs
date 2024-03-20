using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.DTOs;
using BussinessObjects.Enums;
using Microsoft.Identity.Client.Extensions.Msal;
using NftGrpc;
using Repositories.User;
using Services.Common.Gprc.Nft;
using Services.Orchid;

namespace Services.Blockchain
{
    public class BlockchainService : IBlockchainService
    {
        private readonly INftGrpcService _nftGrpcService;
        private readonly IOrchidRepository _orchidRepository;

        public BlockchainService(INftGrpcService nftGrpcService, IOrchidRepository orchidRepository)
        {
            _nftGrpcService = nftGrpcService;
            _orchidRepository = orchidRepository;
        }

        public class DepositForNftException: Exception
        {
            public enum StatusCodeEnum
            {
                AlreadyDeposited
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public DepositForNftException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }
        public async Task<DepositForNftDTO.DepositForNftResponseData> DepositForNft(DepositForNftDTO.DepositForNftRequest request)
        {
            var data = request.Data;
            
            var orchid = await _orchidRepository.GetByIdAsync(data.OrchidId);

                if (orchid.DepositedStatus == DepositStatus.Deposited)
            {
                throw new DepositForNftException(DepositForNftException.StatusCodeEnum.AlreadyDeposited, "Already deposited.");
            }

            orchid.DepositedStatus = DepositStatus.Deposited;
            await _orchidRepository.UpdateAsync(orchid.OrchidId, orchid);

            using (var memoryStream = new MemoryStream())
            {   
                using (var httpClient  = new HttpClient())
                {
                    var bytes = await httpClient.GetByteArrayAsync(orchid.ImageUrl);
                    await memoryStream.WriteAsync(bytes, 0, bytes.Length);
                    memoryStream.Position = 0;
                    var response = await _nftGrpcService.MintNft(new NftGrpc.MintNftRequest()
                    {
                        OrchidId = data.OrchidId.ToString(),
                        Name = orchid.Name,
                        Description = orchid.Description,
                        To = data.Address,
                        Color = orchid.Color,
                        CreatedAt = new DateTimeOffset(orchid.CreatedAt).ToUnixTimeMilliseconds(),
                        UpdatedAt = new DateTimeOffset(orchid.UpdatedAt).ToUnixTimeMilliseconds(),
                        Origin = orchid.Origin,
                        Species = orchid.Species,
                        ImageBytes = Google.Protobuf.ByteString.FromStream(memoryStream)
                    });
                    return new DepositForNftDTO.DepositForNftResponseData()
                    {
                        TransactionHash = response.TransactionHash
                    };
                }
              
                
            }
        }

        public class WithdrawNftException : Exception
        {
            public enum StatusCodeEnum
            {
                NotOwned
            }
            public StatusCodeEnum StatusCode { get; }
            public override string Message { get; }

            public WithdrawNftException(StatusCodeEnum statusCode, string message)
            {
                StatusCode = statusCode;
                Message = message;
            }
        }

        public async Task<WithdawNftDTO.WithdawNftResponseData> WithdrawNft(WithdawNftDTO.WithdawNftRequest request)
        {
            var data = request.Data;
            var userId = request.UserId;

            var response = await _nftGrpcService.BurnNft(new NftGrpc.BurnNftRequest()
            {
                TokenId = data.TokenId
            });

            var orchid = await _orchidRepository.GetByIdAsync(Guid.Parse(response.OrchidId));
            orchid.DepositedStatus = DepositStatus.Available;
            await _orchidRepository.UpdateAsync(orchid.OrchidId, orchid);

            if (orchid.OwnerId != userId) throw new WithdrawNftException(WithdrawNftException.StatusCodeEnum.NotOwned, "You are not the owned of this orchid.");

            return new WithdawNftDTO.WithdawNftResponseData()
            {
                TransactionHash = response.TransactionHash,
            };
        }
    }
}
