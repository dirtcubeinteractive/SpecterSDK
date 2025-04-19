using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Leaderboards
{
    /// <summary>
    /// Represents a request to post a score to one or more leaderboards.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPostScoreToLeaderboardRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of leaderboard IDs where the score will be posted.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
        
        /// <summary>
        /// The score to submit to the leaderboard(s).
        /// </summary>
        public double score { get; set; }
    }
}