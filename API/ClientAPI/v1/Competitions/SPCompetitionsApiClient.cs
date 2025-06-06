using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v1.Competitions
{
    public partial class SPCompetitionsApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPCompetitionsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}