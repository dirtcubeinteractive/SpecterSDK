using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class SPGetMyTaskStatusRequest : SPPaginatedApiRequest
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
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// A boolean flag to include task group tasks in the results.
        /// </summary>
        public bool? includeTaskGroupTasks { get; set; }
        
        /// <summary>
        /// A boolean flag to include inactive tasks in the results.
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
    }

    public class SPGetMyTaskStatusResult : SpecterApiResultBase<SPGetMyTaskStatusResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyTaskStatusResult> GetTaskStatusAsync(SPGetMyTaskStatusRequest request)
        {
            var result = await PostAsync<SPGetMyTaskStatusResult, SPGetMyTaskStatusResponse>("/v2/client/player/me/get-task-status", AuthType, request);
            return result;
        }
    }
}