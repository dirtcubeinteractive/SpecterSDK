using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get instant battle history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerInstantBattleHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// An array of competition IDs to fetch instant battles.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter instant battles.
        /// </summary>
        public List<SPCompetitionStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPInstantBattleHistoryAttribute> attributes { get; set; }
    }
}