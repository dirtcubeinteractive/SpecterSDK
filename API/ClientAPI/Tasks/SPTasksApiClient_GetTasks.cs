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
    public class SPGetTasksRequest: SPApiRequestBaseData, IAttributeConfigurable, IEntityConfigurable
    {
        public List<string> taskIds { get; set; }
        public SPTaskStatus status { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTasksResult : SpecterApiResultBase<SPTaskResponseDataList>
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
        public async Task<SPGetTasksResult> GetTasksAsync(SPGetTasksRequest request)
        {
            var result = await PostAsync<SPGetTasksResult, SPTaskResponseDataList>("/v1/client/task/get-tasks", AuthType, request);
            return result;
        }
    }
}