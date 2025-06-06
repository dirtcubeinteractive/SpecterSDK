using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Competitions
{
    /// <summary>
    /// Represents a request to get competition entries.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCompetitionEntriesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of competition IDs to filter entries.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Array of match IDs to filter entries.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// Array of game IDs to filter entries.
        /// </summary>
        public List<string> gameIds { get; set; }
    }
}