using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskStatusRequest: SPApiRequestBaseData, IAttributeConfigurable, IEntityConfigurable
    {
        public List<string> taskIds { get; set; }
        public SPTaskStatus status { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskStatusResult : SpecterApiResultBase<SPUserTaskResponseDataList>
    {
        public List<SpecterUserTask> UserTasks;
        protected override void InitSpecterObjectsInternal()
        {
            UserTasks = new List<SpecterUserTask>();
            foreach (var taskData in Response.data)
            {
                UserTasks.Add(new SpecterUserTask(taskData));
            }
        }
    }
    
    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskStatusResult> GetTaskStatusAsync(SPGetTaskStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskStatusResult, SPUserTaskResponseDataList>("/v1/client/tasks/get-status", AuthType, request);
            return result;
        }
    }
}