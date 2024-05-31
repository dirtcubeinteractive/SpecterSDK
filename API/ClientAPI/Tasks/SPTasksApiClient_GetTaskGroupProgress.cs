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
    public class SPGetTaskGroupProgressRequest : SPApiRequestBase
    {
        public List<string> taskGroupIds { get; set; }
    }

    public class SPGetTaskGroupProgressResult : SpecterApiResultBase<SPGetTaskGroupProgressResponseData>
    {
        public List<SpecterTaskGroupProgress> TaskGroupProgresses;
        public  int TotalTaskGroupProgressCount;

        protected override void InitSpecterObjectsInternal()
        {
            TotalTaskGroupProgressCount = Response.data.totalCount;

            TaskGroupProgresses = new List<SpecterTaskGroupProgress>();
            foreach (var taskGroupProgress in Response.data.taskGroupProgresses)
            {
                TaskGroupProgresses.Add(new SpecterTaskGroupProgress(taskGroupProgress));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        public async Task<SPGetTaskGroupProgressResult> GetTaskGroupProgressAsync(SPGetTaskGroupProgressRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupProgressResult, SPGetTaskGroupProgressResponseData> ("/v1/client/tasks/get-task-group-progress", AuthType, request);
            return result;
        }
    }
}