using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Stores
{
    public partial class SPStoresApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPStoresApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}