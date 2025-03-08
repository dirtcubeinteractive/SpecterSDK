using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPMissionAttributes
    {
        public const string RewardDetails = "rewardDetails";
        public const string LinkedRewardDetails = "linkedRewardDetails";
        public const string UnlockConditions = "unlockConditions";
        public const string Schedule = "schedule";
        public const string Tasks = "tasks";
        public const string TasksRewardDetails = "tasks.rewardDetails";
        public const string TasksLinkedRewardDetails = "tasks.linkedRewardDetails";
        public const string Meta = "meta";
        public const string Tags = "tags";
    }
    
    /// <summary>
    /// Represents a request to get mission task groups from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMissionsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of task group IDs used to filter the missions.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// If true, inactive tasks within the mission groups will be included.
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
        
        /// <summary>
        /// An array of schedule status values to filter the missions based on their timing or progress.
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Additional tags to filter or annotate missions.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
}