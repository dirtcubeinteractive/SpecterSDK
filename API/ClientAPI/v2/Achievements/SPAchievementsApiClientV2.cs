using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Achievements
{
    public partial class SPAchievementsApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPAchievementsApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}