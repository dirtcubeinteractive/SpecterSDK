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
    public class SPGetTaskGroupsRequest : SPApiRequestBase
    {
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        public List<string> taskGroupIds { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTaskGroupsResult: SpecterApiResultBase<SPGetTaskGroupResponseData>
    {
        public List<SpecterTaskGroup> TaskGroups;
        public int TotalCount;
        protected override void InitSpecterObjectsInternal()
        {
            TaskGroups = new List<SpecterTaskGroup>();
            foreach (var taskGroup in Response.data.taskGroups)
            {
                TaskGroups.Add(new SpecterTaskGroup(taskGroup)); 
            }
            TotalCount = Response.data.totalCount;
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetTaskGroupsResult> GetTaskGroupAsync(SPGetTaskGroupsRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupsResult, SPGetTaskGroupResponseData>("/v1/client/app/get-task-groups", AuthType, request);
            return result;
        }
    }
}