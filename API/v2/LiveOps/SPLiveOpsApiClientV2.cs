using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v2.LiveOps
{
    public partial class SPLiveOpsApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPLiveOpsApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}