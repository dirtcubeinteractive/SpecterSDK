using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Progression
{
    /// <summary>
    /// Represents a request to update a progression marker.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateProgressionMarkerRequest : SPApiRequestBase
    {
        /// <summary>
        /// Identifier for the progression marker to update.
        /// </summary>
        public string progressionMarkerId { get; set; }
        
        /// <summary>
        /// The amount to adjust the progression marker by (always use a positive amount, subtractions are handled by the operation field)
        /// </summary>
        public long amount { get; set; }
        
        /// <summary>
        /// Operation to apply to the progression marker (add or subtract).
        /// </summary>
        public SPOperations operation { get; set; }
        
        /// <summary>
        /// Custom parameters for further customization.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}