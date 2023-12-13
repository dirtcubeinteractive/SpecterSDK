using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPForceCompleteTaskRequest : SPApiRequestBase
    {
        public List<string> taskIds { get; set; }
        public bool getRewardDataInResponse { get; set; }
    }

    public class SPForceCompleteTaskResult : SpecterApiResultBase<SPForceCompleteTaskResponseDataList>
    {
        public List<SpecterForceCompletedTask> ForceCompletedTasks;
        protected override void InitSpecterObjectsInternal()
        {
            ForceCompletedTasks = new List<SpecterForceCompletedTask>();
            foreach (var taskData in Response.data)
            {
                ForceCompletedTasks.Add(new SpecterForceCompletedTask(taskData));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPForceCompleteTaskResult> ForceCompleteTaskAsync(SPForceCompleteTaskRequest request)
        {
            var result = await PostAsync<SPForceCompleteTaskResult, SPForceCompleteTaskResponseDataList>("/v1/client/tasks/force-complete", AuthType, request);
            return result;
        }
    }
}