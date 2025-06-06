using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Leaderboards
{
    /// <summary>
    /// Represents the attributes available for the Leaderboard Results endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPLeaderboardResultsAttribute : SPEnum<SPLeaderboardResultsAttribute>
    {
        public static readonly SPLeaderboardResultsAttribute Rankings = new SPLeaderboardResultsAttribute(0, "rankings", "Rankings");
        public static readonly SPLeaderboardResultsAttribute TotalRanks = new SPLeaderboardResultsAttribute(1, "totalRanks", "Total Ranks");
        
        private SPLeaderboardResultsAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get results for a specific leaderboard.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardResultsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The unique identifier for the leaderboard to retrieve results from.
        /// </summary>
        public string leaderboardId { get; set; }
        
        /// <summary>
        /// The unique instance identifier for the leaderboard session.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPLeaderboardResultsAttribute> attributes { get; set; }
    }
}