using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Competitions
{
    /// <summary>
    /// Represents a request to get tournament result data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTournamentResultRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The unique identifier for the tournament competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// The unique instance identifier for the tournament.
        /// </summary>
        public string instanceId { get; set; }
    }
}