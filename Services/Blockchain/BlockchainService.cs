using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObjects.DTOs;
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

        public async Task<DepositForNftDTO.DepositForNftResponseData> DepositForNft(DepositForNftDTO.DepositForNftRequest request)
        {
            var data = request.Data;
            
            var orchid = await _orchidRepository.GetByIdAsync(data.Json.OrchidId);

            using (var memoryStream = new MemoryStream())
            {
                data.ImageFile.CopyTo(memoryStream);
                memoryStream.Position = 0;
                string destinationObjectName = Path.GetFileName(data.ImageFile.FileName);
                var response = await _nftGrpcService.MintNft(new NftGrpc.MintNftRequest()
                {
                    Name = orchid.Name,
                    Description = orchid.Description,
                    To = data.Json.Address,
                    ImageBytes = Google.Protobuf.ByteString.FromStream(memoryStream)
                });
                return new DepositForNftDTO.DepositForNftResponseData()
                {
                    TransactionHash = response.TransactionHash
                };
            }
        }
    }
}
