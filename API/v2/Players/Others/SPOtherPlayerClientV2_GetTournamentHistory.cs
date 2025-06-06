using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get tournament history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerTournamentHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// ID of the player to retrieve tournament history for.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// An array of competition IDs to fetch tournaments.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Array of schedule statuses to filter tournaments.
        /// </summary>
        public List<SPCompetitionStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTournamentHistoryAttribute> attributes { get; set; }
    }
}