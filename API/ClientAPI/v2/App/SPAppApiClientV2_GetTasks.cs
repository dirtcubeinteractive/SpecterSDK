using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPTaskAttributes
    {
        public const string RewardDetails = "rewardDetails";
        public const string UnlockConditions = "unlockConditions";
        public const string Schedule = "schedule";
        public const string Parameters = "parameters";
        public const string BusinessLogic = "businessLogic";
        public const string LinkedRewardDetails = "linkedRewardDetails";
        public const string Meta = "meta";
        public const string Tags = "tags";
    }
    
    /// <summary>
    /// Represents a request to get tasks from the application with various filtering options.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTasksRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of task IDs to fetch specific tasks.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// If true, include tasks that belong to a task group.
        /// </summary>
        public bool? includeTaskGroupTasks { get; set; }
        
        /// <summary>
        /// An array of tags to filter the tasks by (e.g., 'daily', 'event').
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter tasks.
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; } // TODO: refactor ScheduleStates enum
        
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
}