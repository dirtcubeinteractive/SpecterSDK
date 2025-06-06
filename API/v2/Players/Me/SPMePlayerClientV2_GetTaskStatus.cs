using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get task status information.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskStatusRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of specific task IDs for which to fetch statuses.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// Filter to retrieve tasks with a specific status.
        /// </summary>
        public SPTaskStatus status { get; set; }
        
        /// <summary>
        /// Array of schedule statuses to filter tasks (yet to start | in progress | stopped | expired).
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
        
        /// <summary>
        /// A boolean flag to include task group tasks in the results.
        /// </summary>
        public bool? includeTaskGroupTasks { get; set; }
        
        /// <summary>
        /// A boolean flag to include inactive tasks in the results.
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
    }
}