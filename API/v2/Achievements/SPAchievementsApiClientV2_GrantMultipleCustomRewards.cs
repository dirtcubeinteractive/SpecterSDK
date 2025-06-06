using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant multiple custom rewards grouped by type.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantMultipleCustomRewardsRequest : SPApiRequestBase
    {
        /// <summary>
        /// Object containing rewards grouped by type.
        /// </summary>
        public SPRewardsContainer rewards { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}