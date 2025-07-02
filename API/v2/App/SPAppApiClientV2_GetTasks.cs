using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Tasks endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTaskAttribute : SPEnum<SPTaskAttribute>
    {
        public static readonly SPTaskAttribute RewardDetails = new SPTaskAttribute(0, "rewardDetails", "Reward Details");
        public static readonly SPTaskAttribute UnlockConditions = new SPTaskAttribute(1, "unlockConditions", "Unlock Conditions");
        public static readonly SPTaskAttribute Schedule = new SPTaskAttribute(2, "schedule", "Schedule");
        public static readonly SPTaskAttribute Parameters = new SPTaskAttribute(3, "parameters", "Parameters");
        public static readonly SPTaskAttribute BusinessLogic = new SPTaskAttribute(4, "businessLogic", "Business Logic");
        public static readonly SPTaskAttribute LinkedRewardDetails = new SPTaskAttribute(5, "linkedRewardDetails", "Linked Reward Details");
        public static readonly SPTaskAttribute Meta = new SPTaskAttribute(6, "meta", "Meta");
        public static readonly SPTaskAttribute Tags = new SPTaskAttribute(7, "tags", "Tags");
        
        private SPTaskAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch tasks from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTasksRequestV2 : SPPaginatedApiRequest
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
        /// An array of schedule statuses to filter tasks. Eg usage: SPTaskScheduleStatus.InProgress
        /// </summary>
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes of tasks to include in the response. Eg usage: SPTaskAttribute.RewardDetails
        /// </summary>
        public List<SPTaskAttribute> attributes { get; set; }
    }

    public class SPGetTasksResultV2 : SpecterApiResultBase<SPGetTasksResponse>
    {
        public List<SPTask> Tasks { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Tasks = Response.data?.tasks == null ? new List<SPTask>() : Response.data.tasks.ConvertAll(x => new SPTask(x));
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetTasksResultV2> GetTasksAsync(SPGetTasksRequestV2 request)
        {
            var result = await PostAsync<SPGetTasksResultV2, SPGetTasksResponse>("/v2/client/app/get-tasks", AuthType, request);
            return result;
        }
    }
}