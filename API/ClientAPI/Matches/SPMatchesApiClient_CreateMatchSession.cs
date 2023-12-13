using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Matches
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCreateMatchSessionRequest : SPMatchSessionRequestBase { }
    
    public class SPCreateMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        public async Task<SPCreateMatchSessionResult> CreateMatchSessionAsync(SPCreateMatchSessionRequest request)
        {
            var result = await PostAsync<SPCreateMatchSessionResult, SPMatchSessionResponseData>("/v1/client/matches/create-session", AuthType, request);
            return result;
        }

    }

}
