using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Matches
{
    public class SPMatchSessionRequestBaseData : SPApiRequestBaseData
    {
        public string matchId;
        public List<SPMatchUserInfo> userInfo;
    }
    
    public partial class SPMatchesApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        public SPMatchesApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}