using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Store
{
    public partial class SPStoreApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPStoreApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}