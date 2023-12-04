using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Matches
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPStartMatchSessionRequest : SPMatchSessionRequestBaseData
    {
        public string matchSessionId { get; set; }
    }
    
    public class SPStartMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        public async Task<SPStartMatchSessionResult> StartMatchSessionAsync(SPStartMatchSessionRequest request)
        {
            var result = await PostAsync<SPStartMatchSessionResult, SPMatchSessionResponseData>("/v1/client/match-session/start", AuthType, request);
            return result;
        }

    }

}
