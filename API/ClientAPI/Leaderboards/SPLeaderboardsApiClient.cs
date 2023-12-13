using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Leaderboards
{
    public partial class SPLeaderboardsApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPLeaderboardsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}