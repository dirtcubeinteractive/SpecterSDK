using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to get instant battle results.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetInstantBattleResultRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The unique identifier for the instant battle competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// The unique identifier of the entry in the instant battle.
        /// </summary>
        public string entryId { get; set; }
    }
}