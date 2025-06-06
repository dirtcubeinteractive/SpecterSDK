using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Progression
{
    public partial class SPProgressionApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPProgressionApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}