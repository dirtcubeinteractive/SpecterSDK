using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get reward history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetRewardHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of task IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// An array of task group IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// An array of progression system level IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> levelIds { get; set; }
        
        /// <summary>
        /// An array of leaderboard IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
        
        /// <summary>
        /// An array of competition IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Filter and fetch rewards earned by status.
        /// </summary>
        public SPRewardClaimStatus status { get; set; }
        
        /// <summary>
        /// Additional attributes to fetch with the reward history.
        /// </summary>
        public List<SPRewardHistoryAttribute> attributes { get; set; }
    }
}