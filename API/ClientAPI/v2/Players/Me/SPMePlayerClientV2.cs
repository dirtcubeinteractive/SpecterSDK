using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    public partial class SPMePlayerClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPMePlayerClientV2(SpecterRuntimeConfig config) : base(config)
        {
            
        }
    }
}