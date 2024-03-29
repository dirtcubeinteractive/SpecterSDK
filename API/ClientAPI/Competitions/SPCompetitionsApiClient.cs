using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    public partial class SPCompetitionsApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPCompetitionsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}