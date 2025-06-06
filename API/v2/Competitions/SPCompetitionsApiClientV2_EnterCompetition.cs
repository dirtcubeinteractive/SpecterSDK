using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to enter a competition.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEnterCompetitionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// Optional instance ID for a specific competition instance.
        /// </summary>
        public string competitionInstanceId { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the competition entry.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}