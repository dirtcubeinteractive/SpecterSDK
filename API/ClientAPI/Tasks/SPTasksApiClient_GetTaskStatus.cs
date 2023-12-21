using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskStatusRequest: SPApiRequestBase, IAttributeConfigurable, IEntityConfigurable
    {
        public List<string> taskIds { get; set; }
        public SPTaskStatus status { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskStatusResult : SpecterApiResultBase<SPTaskStatusResponseDataList>
    {
        public List<SpecterTaskStatus> TaskStatuses;
        protected override void InitSpecterObjectsInternal()
        {
            TaskStatuses = new List<SpecterTaskStatus>();
            foreach (var taskData in Response.data)
            {
                TaskStatuses.Add(new SpecterTaskStatus(taskData));
            }
        }
    }
    
    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskStatusResult> GetTaskStatusAsync(SPGetTaskStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskStatusResult, SPTaskStatusResponseDataList>("/v1/client/tasks/get-status", AuthType, request);
            return result;
        }
    }
}