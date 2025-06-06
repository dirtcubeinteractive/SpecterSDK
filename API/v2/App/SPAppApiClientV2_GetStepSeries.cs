using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Step Series endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPStepSeriesAttribute : SPEnum<SPStepSeriesAttribute>
    {
        public static readonly SPStepSeriesAttribute TaskRewardDetails = new SPStepSeriesAttribute(0, "tasks.rewardDetails", "Task Reward Details");
        public static readonly SPStepSeriesAttribute TaskLinkedRewardDetails = new SPStepSeriesAttribute(1, "tasks.linkedRewardDetails", "Task Linked Reward Details");
        public static readonly SPStepSeriesAttribute Tasks = new SPStepSeriesAttribute(2, "tasks", "Tasks");
        public static readonly SPStepSeriesAttribute UnlockConditions = new SPStepSeriesAttribute(3, "unlockConditions", "Unlock Conditions");
        public static readonly SPStepSeriesAttribute Schedule = new SPStepSeriesAttribute(4, "schedule", "Schedule");
        public static readonly SPStepSeriesAttribute Meta = new SPStepSeriesAttribute(5, "meta", "Meta");
        public static readonly SPStepSeriesAttribute Tags = new SPStepSeriesAttribute(6, "tags", "Tags");
        
        private SPStepSeriesAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
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
        /// An array of schedule statuses to filter the step series tasks. Eg usage: SPTaskScheduleStatus.InProgress
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
        
        /// <summary>
        /// An array of tags to further filter or categorize the step series.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of step series to include in the response. Eg usage: SPStepSeriesAttribute.Tasks
        /// </summary>
        public List<SPStepSeriesAttribute> attributes { get; set; }
    }

    public class SPGetStepSeriesResult : SpecterApiResultBase<SPGetStepSeriesResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetStepSeriesResult> GetStepSeriesAsync(SPGetStepSeriesRequest request)
        {
            var result = await PostAsync<SPGetStepSeriesResult, SPGetStepSeriesResponse>("/v2/client/app/get-step-series", AuthType, request);
            return result;
        }
    }
}