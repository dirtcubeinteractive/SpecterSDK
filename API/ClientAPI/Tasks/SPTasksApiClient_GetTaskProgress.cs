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
    /// <summary>
    /// Represents the request structure to get the user's progress across all tasks configured
    /// on the dashboard. Tasks belonging to tasks groups can also be queried by using filters provided in the request.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetTaskProgressRequest"/> class represents a request to get a user's task progresses from the Specter Tasks API.
    /// It can be used to specify the filter criteria for the tasks for which progresses are to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTaskProgressAsync method in the <see cref="SPTasksApiClient"/> class to retrieve task progress information for the user from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskProgressRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// A list of dashboard specific task group IDs to limit retrieval of task progresses to specific tasks.
        /// </summary>
        public List<string> taskIds { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTaskProgressAsync method of the <see cref="SPTasksApiClient"/> class.
    /// </summary>
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
        /// <summary>
        /// Get the task progresses for a user asynchronously.
        /// <remarks>
        /// For full information about the get task progress endpoint, see the Tasks section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTaskProgressRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetTaskProgressResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTaskProgressResult> GetTaskProgressAsync(SPGetTaskProgressRequest request)
        {
            var result = await PostAsync<SPGetTaskProgressResult, SPGetTaskProgressResponseData>("/v1/client/tasks/get-progress", AuthType, request);
            return result;
        }
    }
}