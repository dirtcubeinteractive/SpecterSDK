using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Missions endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPMissionAttribute : SPEnum<SPMissionAttribute>
    {
        public static readonly SPMissionAttribute RewardDetails = new SPMissionAttribute(0, "rewardDetails", "Reward Details");
        public static readonly SPMissionAttribute UnlockConditions = new SPMissionAttribute(1, "unlockConditions", "Unlock Conditions");
        public static readonly SPMissionAttribute Schedule = new SPMissionAttribute(2, "schedule", "Schedule");
        public static readonly SPMissionAttribute Tasks = new SPMissionAttribute(3, "tasks", "Tasks");
        public static readonly SPMissionAttribute LinkedRewardDetails = new SPMissionAttribute(4, "linkedRewardDetails", "Linked Reward Details");
        public static readonly SPMissionAttribute Meta = new SPMissionAttribute(5, "meta", "Meta");
        public static readonly SPMissionAttribute Tags = new SPMissionAttribute(6, "tags", "Tags");
        public static readonly SPMissionAttribute TaskRewardDetails = new SPMissionAttribute(7, "tasks.rewardDetails", "Task Reward Details");
        public static readonly SPMissionAttribute TaskLinkedRewardDetails = new SPMissionAttribute(8, "tasks.linkedRewardDetails", "Task Linked Reward Details");
        
        private SPMissionAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
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
        /// An array of schedule status values to filter the missions based on their timing or progress. Eg usage: SPTaskScheduleStatus.InProgress
        /// </summary>
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Additional tags to filter or annotate missions.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of missions to include in the response. Eg usage: SPMissionAttribute.Tasks
        /// </summary>
        public List<SPMissionAttribute> attributes { get; set; }
    }

    public class SPGetMissionsResult : SpecterApiResultBase<SPGetMissionsResponse>
    {
        public List<SPMission> Missions { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Missions = Response.data?.missions == null ? new List<SPMission>() : Response.data.missions.ConvertAll(x => new SPMission(x));
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetMissionsResult> GetMissionsAsync(SPGetMissionsRequest request)
        {
            var result = await PostAsync<SPGetMissionsResult, SPGetMissionsResponse>("/v2/client/app/get-missions", AuthType, request);
            return result;
        }
    }
}