using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Leaderboards
{
    public partial class SPLeaderboardApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPLeaderboardApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}