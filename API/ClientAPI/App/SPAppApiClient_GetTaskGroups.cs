using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupsRequest : SPApiRequestBaseData
    {
        public List<string> taskGroupIds { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskGroupsResult: SpecterApiResultBase<SPTaskGroupResponseDataList>
    {
        public List<SpecterTaskGroup> TaskGroups;
        protected override void InitSpecterObjectsInternal()
        {
            TaskGroups = new();
            foreach (var taskGroup in Response.data)
            {
                TaskGroups.Add(new SpecterTaskGroup(taskGroup)); 
            }
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetTaskGroupsResult> GetTaskGroupAsync(SPGetTaskGroupsRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupsResult, SPTaskGroupResponseDataList>("/v1/client/app/get-task-groups", AuthType, request);
            return result;
        }
    }
}