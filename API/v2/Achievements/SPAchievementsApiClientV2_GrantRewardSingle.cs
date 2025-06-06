using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant a single reward.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardSingleRequest : SPApiRequestBase
    {
        /// <summary>
        /// Type of the reward (e.g., item, bundle).
        /// </summary>
        public SPRewardType type { get; set; }
        
        /// <summary>
        /// Unique identifier of the reward.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the reward to grant.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Flag to bypass lock condition.
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to bypass limited edition restriction.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}