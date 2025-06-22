using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get match history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyMatchHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The ID of the match session to fetch.
        /// </summary>
        public string matchSessionId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPMatchHistoryAttribute> attributes { get; set; }
    }

    public class SPGetMyMatchHistoryResult : SpecterApiResultBase<SPGetMyMatchHistoryResponse>
    {
        public List<SPMatchHistoryEntry> MatchHistory { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MatchHistory = Response.data?.ConvertAll(x => new SPMatchHistoryEntry(x)) ?? new List<SPMatchHistoryEntry>();
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyMatchHistoryResult> GetMatchHistoryAsync(SPGetMyMatchHistoryRequest request)
        {
            var result = await PostAsync<SPGetMyMatchHistoryResult, SPGetMyMatchHistoryResponse>("/v2/client/player/me/get-match-history", AuthType, request);
            return result;
        }
    }
}