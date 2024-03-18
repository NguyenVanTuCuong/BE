using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Gprc.Nft
{
    public interface INftGrpcService
    {
        public Task<NftGrpc.MintNftResponse> MintNft(NftGrpc.MintNftRequest request);
    }
}
