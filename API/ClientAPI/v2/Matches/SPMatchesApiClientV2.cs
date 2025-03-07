using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Matches
{
    public class SPMatchesApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPMatchesApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}