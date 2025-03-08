using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPStepSeriesAttributes
    {
        public const string Tasks = "tasks";
        public const string TasksRewardDetails = "tasks.rewardDetails";
        public const string TasksLinkedRewardDetails = "tasks.linkedRewardDetails";
        public const string UnlockConditions = "unlockConditions";
        public const string Schedule = "schedule";
        public const string Meta = "meta";
        public const string Tags = "tags";
    }
    
    /// <summary>
    /// Represents a request to get step series task groups from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStepSeriesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of task group IDs that identify the step series.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// If true, the response includes tasks that are not currently active in the series.
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter the step series tasks.
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
        
        /// <summary>
        /// An array of tags to further filter or categorize the step series.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
}