using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v2.Events
{
    public partial class SPEventsApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPEventsApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}