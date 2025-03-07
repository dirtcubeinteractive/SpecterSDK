using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Account
{
    public partial class SPAccountApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPAccountApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}