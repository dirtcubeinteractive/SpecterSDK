using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to get the current prize pool for a RMG competition.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetPrizePoolRequest : SPApiRequestBase
    {
        /// <summary>
        /// Unique identifier for the competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// Identifier for the specific instance of the competition.
        /// </summary>
        public string instanceId { get; set; }
    }
}