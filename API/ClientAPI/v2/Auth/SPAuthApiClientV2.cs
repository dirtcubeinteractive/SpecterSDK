using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Auth
{
    public partial class SPAuthApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;
        
        public SPAuthApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}