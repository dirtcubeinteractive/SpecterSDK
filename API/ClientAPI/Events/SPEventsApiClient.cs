using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Events
{
    public partial class SPEventsApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPEventsApiClient(SpecterRuntimeConfig config) : base(config) {}
    }
}
