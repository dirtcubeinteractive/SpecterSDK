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
        public List<SpecterForceCompletedTaskInfo> ForceCompletedTaskInfos;
        protected override void InitSpecterObjectsInternal()
        {
            ForceCompletedTaskInfos = new List<SpecterForceCompletedTaskInfo>();
            foreach (var taskData in Response.data)
            {
                ForceCompletedTaskInfos.Add(new SpecterForceCompletedTaskInfo(taskData));
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