using Grpc.Core;
using Grpc.Net.Client;
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

        public async Task<NftGrpc.MintNftResponse> MintNft(NftGrpc.MintNftRequest request)
        {
            return await client.MintNftAsync(request);
        } 
    }
}
