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
    public class SPGetTasksRequest : SPPaginatedApiRequest
    {
        public List<string> taskIds { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTasksResult : SpecterApiResultBase<SPGetTasksResponseData>
    {
        public List<SpecterTask> Tasks;
        public int TotalTaskCount;

        protected override void InitSpecterObjectsInternal()
        {
            Tasks = new List<SpecterTask>();
            foreach (var taskData in Response.data.tasks)
            {
                Tasks.Add(new SpecterTask(taskData));
            }
            TotalTaskCount = Response.data.totalCount;
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetTasksResult> GetTasksAsync(SPGetTasksRequest request)
        {
            var result = await PostAsync<SPGetTasksResult, SPGetTasksResponseData>("/v1/client/app/get-tasks", AuthType, request);
            return result;
        }
    }
}