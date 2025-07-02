using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Leaderboards
{
    /// <summary>
    /// Represents the attributes available for the Leaderboard Rankings endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPLeaderboardRankingsAttribute : SPEnum<SPLeaderboardRankingsAttribute>
    {
        public static readonly SPLeaderboardRankingsAttribute Rankings = new SPLeaderboardRankingsAttribute(0, "rankings", "Rankings");
        public static readonly SPLeaderboardRankingsAttribute TotalRanks = new SPLeaderboardRankingsAttribute(1, "totalRanks", "Total Ranks");
        
        private SPLeaderboardRankingsAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get rankings for a specific leaderboard.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardRankingsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The unique identifier for the leaderboard to retrieve rankings from.
        /// </summary>
        public string leaderboardId { get; set; }
        
        /// <summary>
        /// The unique instance identifier for the leaderboard session.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPLeaderboardRankingsAttribute> attributes { get; set; }
    }

    public class SPGetLeaderboardRankingsResult : SpecterApiResultBase<SPGetLeaderboardRankingsResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPLeaderboardApiClientV2
    {
        public async Task<SPGetLeaderboardRankingsResult> GetLeaderboardRankingsAsync(SPGetLeaderboardRankingsRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardRankingsResult, SPGetLeaderboardRankingsResponse>("/v2/client/leaderboards/get-rankings", AuthType, request);
            return result;
        }
    }
}