using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.v2.Inventory
{
    public partial class SPInventoryApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPInventoryApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}