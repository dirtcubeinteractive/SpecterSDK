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
    /// Represents a request to get task group status information for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerTaskGroupStatusRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// An optional array of specific task group IDs for which to fetch statuses.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// An optional array of task group types to filter out the retrieved statuses by. (mission | step series)
        /// </summary>
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter retrieved task group statuses.
        /// </summary>
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// A boolean flag to include inactive tasks in the results (eg: useful to fetch inactive missions in a Mission group).
        /// </summary>
        public bool? includeInactiveTasks { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTaskGroupStatusAttribute> attributes { get; set; }
    }
    
    public class SPGetOtherPlayerTaskGroupStatusResult : SpecterApiResultBase<SPGetOtherPlayerTaskGroupStatusResponse>
    {
        public List<SPTaskGroupStatusInfo> StatusInfos { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            StatusInfos = Response.data?.ConvertAll(x => new SPTaskGroupStatusInfo(x)) ?? new List<SPTaskGroupStatusInfo>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerTaskGroupStatusResult> GetTaskGroupStatusAsync(SPGetOtherPlayerTaskGroupStatusRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerTaskGroupStatusResult, SPGetOtherPlayerTaskGroupStatusResponse>("/v2/client/player/get-task-group-status", AuthType, request);
            return result;
        }
    }
}