using SpecterSDK.API.ClientAPI.v2.Players.Me;
using SpecterSDK.API.ClientAPI.v2.Players.Others;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Players
{
    public class SPPlayersApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPOtherPlayerClientV2 Other { get; private set; }
        public SPMePlayerClientV2 Me { get; private set; }
        
        public SPPlayersApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
            Other = new SPOtherPlayerClientV2(config);
            Me = new SPMePlayerClientV2(config);
        }
    }
}