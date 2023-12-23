using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupStatusRequest : SPPaginatedApiRequest
    {
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        public List<string> taskGroupIds { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskGroupStatusResult : SpecterApiResultBase<SPTaskGroupStatusResponseDataList>
    {
        public List<SpecterTaskGroupStatus> TaskGroupStatuses;
        protected override void InitSpecterObjectsInternal()
        {
            TaskGroupStatuses = new List<SpecterTaskGroupStatus>();
            foreach (var taskGroup in Response.data)
            {
                TaskGroupStatuses.Add(new SpecterTaskGroupStatus(taskGroup));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskGroupStatusResult> GetTaskGroupStatusAsync(SPGetTaskGroupStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupStatusResult, SPTaskGroupStatusResponseDataList>("/v1/client/tasks/get-task-group-status", AuthType, request);
            return result;
        }
    }
}