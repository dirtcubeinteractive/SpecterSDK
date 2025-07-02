using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v2.Account
{
    public partial class SPAccountApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPAccountApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}