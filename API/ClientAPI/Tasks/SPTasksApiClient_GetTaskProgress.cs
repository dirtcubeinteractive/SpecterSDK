using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskProgressRequest : SPApiRequestBase
    {
        public List<string> taskIds { get; set; }
    }

    public class SPGetTaskProgressResult : SpecterApiResultBase<SPGetTaskProgressResponseData>
    {
        public List<SpecterTaskProgress> TaskProgresses;
        public int TotalTaskProgressCount;

        protected override void InitSpecterObjectsInternal()
        {
            TotalTaskProgressCount = Response.data.totalCount;

            TaskProgresses = new List<SpecterTaskProgress>();
            foreach (var taskProgress in Response.data.taskProgresses)
            {
                TaskProgresses.Add(new SpecterTaskProgress(taskProgress));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskProgressResult> GetTaskProgressAsync(SPGetTaskProgressRequest request)
        {
            var result = await PostAsync<SPGetTaskProgressResult, SPGetTaskProgressResponseData>("/v1/client/tasks/get-progress", AuthType, request);
            return result;
        }
    }
}