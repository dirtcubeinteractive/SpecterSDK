using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Stores
{
    public partial class SPStoreApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPStoreApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}