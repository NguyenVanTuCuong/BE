using Grpc.Core;
using Grpc.Net.Client;
using NftGrpc;
namespace Services.Common.Gprc.Nft
{
    public class NftGrpcService : INftGrpcService
    {
        readonly NftGrpc.NftService.NftServiceClient client;

        public NftGrpcService()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            client = new NftGrpc.NftService.NftServiceClient(channel);
        }

        public async Task<BurnNftResponse> BurnNft(BurnNftRequest request)
        {
            return await client.BurnNftAsync(request);
        }

        public async Task<NftGrpc.MintNftResponse> MintNft(NftGrpc.MintNftRequest request)
        {
            return await client.MintNftAsync(request);
        } 
    }
}
