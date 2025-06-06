using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.v2.RealMoneyGaming
{
    public partial class SPRmgApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPRmgApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}