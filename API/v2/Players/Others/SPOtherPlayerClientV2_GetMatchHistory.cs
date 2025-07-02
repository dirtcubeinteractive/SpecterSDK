using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get match history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerMatchHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPMatchHistoryAttribute> attributes { get; set; }
    }
    
    public class SPGetOtherPlayerMatchHistoryResult : SpecterApiResultBase<SPGetOtherPlayerMatchHistoryResponse>
    {
        public List<SPMatchHistoryEntry> MatchHistory { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MatchHistory = Response.data?.ConvertAll(x => new SPMatchHistoryEntry(x)) ?? new List<SPMatchHistoryEntry>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerMatchHistoryResult> GetMatchHistoryAsync(SPGetOtherPlayerMatchHistoryRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerMatchHistoryResult, SPGetOtherPlayerMatchHistoryResponse>("/v2/client/player/get-match-history", AuthType, request);
            return result;
        }
    }
}