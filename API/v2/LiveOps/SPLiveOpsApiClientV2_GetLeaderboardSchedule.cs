using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.LiveOps
{
    /// <summary>
    /// Represents a request to retrieve schedule history for specified leaderboards.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardScheduleRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of leaderboard IDs for which to retrieve schedule history.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
    }
}