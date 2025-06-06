using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Achievements
{
    /// <summary>
    /// Represents a request to force the completion of a task.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPForceCompleteTaskRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array containing a single task ID to force completion. Must contain exactly one ID.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// Flag to include reward data in the response if set to true.
        /// </summary>
        public bool? getRewardDataInResponse { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}