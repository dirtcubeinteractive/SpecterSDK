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
    public class SPForceCompleteTaskRequest : SPApiRequestBaseData
    {
        public List<string> taskIds { get; set; }

        public bool getRewardDataInResponse { get; set; }
    }

    public class SPForceCompleteTaskResult : SpecterApiResultBase<SPTaskResponseDataList>
    {
        public List<SpecterTask> Tasks;
        protected override void InitSpecterObjectsInternal()
        {
            Tasks = new List<SpecterTask>();
            foreach (var taskData in Response.data)
            {
                Tasks.Add(new SpecterTask(taskData));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPForceCompleteTaskResult> ForceCompleteTaskAsync(SPForceCompleteTaskRequest request)
        {
            var result = await PostAsync<SPForceCompleteTaskResult, SPTaskResponseDataList>("/v1/client/tasks/force-complete", AuthType, request);
            return result;
        }
    }
}