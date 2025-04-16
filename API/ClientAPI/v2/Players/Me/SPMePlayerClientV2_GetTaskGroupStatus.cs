using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Task Group Status endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTaskGroupStatusAttribute : SPEnum<SPTaskGroupStatusAttribute>
    {
        public static readonly SPTaskGroupStatusAttribute Tasks = new SPTaskGroupStatusAttribute(0, "tasks", "Tasks");
        
        private SPTaskGroupStatusAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get task group status information.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupStatusRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An optional array of specific task group IDs for which to fetch statuses.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// An optional array of task group types to filter out the retrieved statuses by. (mission | step series)
        /// </summary>
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter retrieved task group statuses.
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
        
        /// <summary>
        /// A boolean flag to include inactive tasks in the results (eg: useful to fetch inactive missions in a Mission group).
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTaskGroupStatusAttribute> attributes { get; set; }
    }
}