using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Tasks
{
    /// <summary>
    /// Represents the request structure to get the user's progress across all task groups and their associated task event params
    /// as configured by you on the Specter dashboard.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetTaskGroupProgressRequest"/> class represents a request to get a user's task group task progresses from the Specter Tasks API.
    /// It can be used to specify the filter criteria for the task groups for which corresponding task progresses are to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTaskGroupProgressAsync method in the <see cref="SPTasksApiClient"/> class to retrieve task group progress information for the user from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupProgressRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// A list of dashboard specific task group IDs to limit retrieval of task group progresses to specific task groups.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        public bool includeInactiveTasks { get; set; }
        public List<SPTasksScheduleStatus> scheduleStatuses { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTaskGroupProgressAsync method of the <see cref="SPTasksApiClient"/> class.
    /// </summary>
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
        /// <summary>
        /// Get the task group and task progresses for a user asynchronously.
        /// <remarks>
        /// For full information about the get task group progress endpoint, see the Tasks section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTaskGroupProgressRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetTaskGroupProgressResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTaskGroupProgressResult> GetTaskGroupProgressAsync(SPGetTaskGroupProgressRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupProgressResult, SPGetTaskGroupProgressResponseData> ("/v1/client/tasks/get-task-group-progress", AuthType, request);
            return result;
        }
    }
}