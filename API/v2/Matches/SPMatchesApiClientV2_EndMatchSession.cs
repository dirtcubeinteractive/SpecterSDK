using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Matches
{
    /// <summary>
    /// Represents a request to end an active match session.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEndMatchSessionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the match session to be ended.
        /// </summary>
        public string matchSessionId { get; set; }
        
        /// <summary>
        /// An array of user objects involved in the session, including their outcomes.
        /// </summary>
        public List<SPEndMatchUserInfoV2> userInfo { get; set; }
    }

    public class SPEndMatchSessionResult : SpecterApiResultBase<SPEndMatchSessionResponse>
    {
        public string MatchSessionId { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClientV2
    {
        public async Task<SPEndMatchSessionResult> EndMatchSessionAsync(SPEndMatchSessionRequest request)
        {
            var result = await PostAsync<SPEndMatchSessionResult, SPEndMatchSessionResponse>("/v2/client/matches/end-session", AuthType, request);
            return result;
        }
    }
}