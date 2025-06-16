using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get task progress information.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyTaskProgressRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of task IDs for which to retrieve progress.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// Filter to retrieve tasks by status.
        /// </summary>
        public SPTaskStatus status { get; set; }
        
        /// <summary>
        /// Array of schedule statuses to filter tasks.
        /// </summary>
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Include tasks from task groups if set to true.
        /// </summary>
        public bool? includeTaskGroupTasks { get; set; }
    }

    public class SPGetMyTaskProgressResult : SpecterApiResultBase<SPGetMyTaskProgressResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyTaskProgressResult> GetTaskProgressAsync(SPGetMyTaskProgressRequest request)
        {
            var result = await PostAsync<SPGetMyTaskProgressResult, SPGetMyTaskProgressResponse>("/v2/client/player/me/get-task-progress", AuthType, request);
            return result;
        }
    }
}