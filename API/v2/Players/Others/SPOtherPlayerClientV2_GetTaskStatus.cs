using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get task status information for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerTaskStatusRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
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
    
    public class SPGetOtherPlayerTaskStatusResult : SpecterApiResultBase<SPGetOtherPlayerTaskStatusResponse>
    {
        public List<SPTaskStatusInfo> StatusInfos { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            StatusInfos = Response.data?.ConvertAll(x => new SPTaskStatusInfo(x)) ?? new List<SPTaskStatusInfo>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerTaskStatusResult> GetTaskStatusAsync(SPGetOtherPlayerTaskStatusRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerTaskStatusResult, SPGetOtherPlayerTaskStatusResponse>("/v2/client/player/get-task-status", AuthType, request);
            return result;
        }
    }
}