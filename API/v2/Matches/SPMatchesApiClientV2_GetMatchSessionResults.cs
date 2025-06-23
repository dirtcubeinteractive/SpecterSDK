using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Matches
{
    /// <summary>
    /// Represents a request to retrieve detailed information about specific match sessions.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchSessionResultsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of match session IDs to retrieve specific match session details.
        /// If provided, other filters are ignored.
        /// </summary>
        public List<string> matchSessionIds { get; set; }
        
        /// <summary>
        /// Array of competition IDs to filter match sessions by competition.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Array of match IDs to filter match sessions by specific matches.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// Array of game IDs to filter match sessions by specific games.
        /// </summary>
        public List<string> gameIds { get; set; }
    }

    public class SPGetMatchSessionResultsResult : SpecterApiResultBase<SPGetMatchSessionResultsResponse>
    {
        public List<SPMatchSessionResultInfo> MatchSessions { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MatchSessions = Response.data?.ConvertAll(x => new SPMatchSessionResultInfo(x)) ?? new List<SPMatchSessionResultInfo>();
        }
    }

    public partial class SPMatchesApiClientV2
    {
        public async Task<SPGetMatchSessionResultsResult> GetMatchSessionResultsAsync(SPGetMatchSessionResultsRequest request)
        {
            var result = await PostAsync<SPGetMatchSessionResultsResult, SPGetMatchSessionResultsResponse>("/v2/client/matches/get-session-details", AuthType, request);
            return result;
        }
    }
}