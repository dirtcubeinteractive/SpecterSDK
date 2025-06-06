using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.v2.Wallet
{
    public partial class SPWalletApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPWalletApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}