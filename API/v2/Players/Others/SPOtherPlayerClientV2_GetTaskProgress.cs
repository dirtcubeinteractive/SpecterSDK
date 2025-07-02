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
    /// Represents a request to get task progress for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerTaskProgressRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
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
    
    public class SPGetOtherPlayerTaskProgressResult : SpecterApiResultBase<SPGetOtherPlayerTaskProgressResponse>
    {
        public List<SPTaskProgressInfo> TaskProgressInfos { get; set; }
        public int TotalCount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            TaskProgressInfos = Response.data?.taskProgresses == null ? new List<SPTaskProgressInfo>() : Response.data.taskProgresses.ConvertAll(x => new SPTaskProgressInfo(x));
            TotalCount = Response.data?.totalCount ?? 0;
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerTaskProgressResult> GetTaskProgressAsync(SPGetOtherPlayerTaskProgressRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerTaskProgressResult, SPGetOtherPlayerTaskProgressResponse>("/v2/client/player/get-task-progress", AuthType, request);
            return result;
        }
    }
}