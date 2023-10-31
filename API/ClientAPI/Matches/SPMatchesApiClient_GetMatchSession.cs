using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Matches
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchSessionRequest : SPMatchSessionRequestBaseData { }
    
    public class SPGetMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        public async Task<SPGetMatchSessionResult> GetMatchSessionAsync(SPGetMatchSessionRequest request)
        {
            var result = await PostAsync<SPGetMatchSessionResult, SPMatchSessionResponseData>("/v1/client/match-session/get-match-session-details", AuthType, request);
            return result;
        }

    }

}
