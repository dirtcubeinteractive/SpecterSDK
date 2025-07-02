using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to force the completion of a task.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPForceCompleteTaskRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array containing a single task ID to force completion. Must contain exactly one ID.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// Flag to include reward data in the response if set to true.
        /// </summary>
        public bool? getRewardDataInResponse { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPForceCompleteTaskResult : SpecterApiResultBase<SPForceCompleteTaskResponse>
    {
        public List<SPForceCompletedTaskInfo> ForceCompletedTasks { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            ForceCompletedTasks = Response.data?.ConvertAll(x => new SPForceCompletedTaskInfo(x));
        }
    }

    public partial class SPAchievementsApiClientV2
    {
        public async Task<SPForceCompleteTaskResult> ForceCompleteTaskAsync(SPForceCompleteTaskRequest request)
        {
            var result = await PostAsync<SPForceCompleteTaskResult, SPForceCompleteTaskResponse>("/v2/client/achievements/force-complete", AuthType, request);
            return result;
        }
    }
}