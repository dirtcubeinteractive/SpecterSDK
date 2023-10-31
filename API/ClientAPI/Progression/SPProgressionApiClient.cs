using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Progression
{
    public partial class SPProgressionApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPProgressionApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}