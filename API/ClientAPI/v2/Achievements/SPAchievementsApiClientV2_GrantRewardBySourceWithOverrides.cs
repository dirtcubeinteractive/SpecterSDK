using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant rewards from a source with custom overrides.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardBySourceWithOverridesRequest : SPApiRequestBase
    {
        /// <summary>
        /// An object representing the origin of the rewards.
        /// </summary>
        public SPRewardSourceWithOverrides source { get; set; }
    }
    
    /// <summary>
    /// Represents a reward source object with override capabilities.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardSourceWithOverrides
    {
        /// <summary>
        /// The unique identifier for the source.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// The type of source, e.g., task or level.
        /// </summary>
        public SPRewardSourceType type { get; set; }
        
        /// <summary>
        /// Optional unique identifier for the source instance.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Flag to bypass lock condition.
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to bypass limited edition restriction.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
        
        /// <summary>
        /// Object containing custom reward configurations.
        /// </summary>
        public SPRewardsContainer overrides { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}