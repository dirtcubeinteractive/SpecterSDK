using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    public partial class SPRewardsApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPRewardsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}
