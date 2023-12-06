using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Matches
{
    [Serializable]
    public class SPEndMatchSessionRequest : SPApiRequestBaseData
    {
        public string matchSessionId;
        public List<SPEndMatchSessionUserInfo> userInfo;
    }

    public class SPEndMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        public async Task<SPEndMatchSessionResult> EndMatchSessionAsync(SPEndMatchSessionRequest request)
        {
            var result = await PostAsync<SPEndMatchSessionResult, SPMatchSessionResponseData>("/v1/client/matches/end-session", AuthType, request);
            return result;
        }

    }
}
