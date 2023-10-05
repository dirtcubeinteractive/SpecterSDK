using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Wallet
{
    public partial class SPWalletApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPWalletApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}