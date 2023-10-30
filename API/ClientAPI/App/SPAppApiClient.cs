using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.App
{
    public partial class SPAppApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPAppApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}