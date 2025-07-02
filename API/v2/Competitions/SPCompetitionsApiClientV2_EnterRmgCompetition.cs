using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to enter a Real Money Gaming competition.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEnterRmgCompetitionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the competition. This field is required.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// Optional identifier for a specific instance of the competition.
        /// </summary>
        public string competitionInstanceId { get; set; }
        
        /// <summary>
        /// Optional identifier for the competition entry.
        /// </summary>
        public string entryId { get; set; }
        
        /// <summary>
        /// Optional object containing additional custom parameters for the competition entry.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}