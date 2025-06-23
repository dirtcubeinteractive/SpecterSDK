using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Matches
{
    /// <summary>
    /// Represents a request to create a new match session.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCreateMatchSessionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the match for which the session is being created.
        /// </summary>
        public string matchId { get; set; }
        
        /// <summary>
        /// Identifier for the competition associated with the match session.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// An array containing details of users participating in the session.
        /// </summary>
        public List<SPMatchUserInfoV2> userInfo { get; set; }
    }

    public class SPCreateMatchSessionResult : SpecterApiResultBase<SPCreateMatchSessionResponse>
    {
        public string MatchSessionId { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClientV2
    {
        public async Task<SPCreateMatchSessionResult> CreateMatchSessionAsync(SPCreateMatchSessionRequest request)
        {
            var result = await PostAsync<SPCreateMatchSessionResult, SPCreateMatchSessionResponse>("/v2/client/matches/create-session", AuthType, request);
            return result;
        }
    }
}