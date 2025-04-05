using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Achievements
{
    /// <summary>
    /// Represents a reward source object.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardSource
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
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents a request to grant rewards from multiple sources.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardBySourceRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of source objects representing the origin of the rewards.
        /// </summary>
        public List<SPRewardSource> sources { get; set; }
    }
}