using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Games
{
    public partial class SPGamesApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPGamesApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}