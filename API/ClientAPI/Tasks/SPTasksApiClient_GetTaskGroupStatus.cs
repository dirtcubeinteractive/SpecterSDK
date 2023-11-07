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
    public class SPGetTaskGroupStatusRequest : SPApiRequestBaseData
    {
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        public List<string> taskGroupIds { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskGroupStatusResult : SpecterApiResultBase<SPUserTaskGroupResponseDataList>
    {
        public List<SpecterUserTaskGroup> UserTaskGroups;
        protected override void InitSpecterObjectsInternal()
        {
            UserTaskGroups = new List<SpecterUserTaskGroup>();
            foreach (var taskGroup in Response.data)
            {
                UserTaskGroups.Add(new SpecterUserTaskGroup(taskGroup));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskGroupStatusResult> GetTaskGroupStatusAsync(SPGetTaskGroupStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupStatusResult, SPUserTaskGroupResponseDataList>("/v1/client/tasks/get-task-group-status", AuthType, request);
            return result;
        }
    }
}