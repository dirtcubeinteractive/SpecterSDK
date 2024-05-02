using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    /// <summary>
    /// Represents the request structure to get the user's completion and reward claim status across all standalone tasks configured
    /// on the dashboard and not associated with any task groups.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetTaskStatusRequest"/> class represents a request to get a user's standalone task statuses from the Specter Tasks API.
    /// It can be used to specify the filter criteria for the tasks for which statuses are to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTaskStatusAsync method in the <see cref="SPTasksApiClient"/> class to retrieve task status information for the user from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskStatusRequest: SPPaginatedApiRequest
    {
        /// <summary>
        /// A list of dashboard specific task group IDs to limit retrieval of task group statuses to specific task groups.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// A filter of type <see cref="SPTaskStatus"/> to limit retrieval of tasks only to a specific status.
        /// <example>"completed"</example>
        /// </summary>
        public SPTaskStatus status { get; set; }
        
        /// <summary>
        /// Flag to include even tasks that belong to task groups
        /// </summary>
        public bool includeTaskGroupTasks { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTaskStatusAsync method of the <see cref="SPTasksApiClient"/> class.
    /// </summary>
    public class SPGetTaskStatusResult : SpecterApiResultBase<SPTaskStatusResponseDataList>
    {
        // List of task statuses fetched.
        public List<SpecterTaskStatus> TaskStatuses;
        
        protected override void InitSpecterObjectsInternal()
        {
            TaskStatuses = new List<SpecterTaskStatus>();
            foreach (var taskData in Response.data)
            {
                TaskStatuses.Add(new SpecterTaskStatus(taskData));
            }
        }
    }
    
    public partial class SPTasksApiClient
    {
        /// <summary>
        /// Get the task statuses for a user asynchronously.
        /// <remarks>
        /// For full information about the get task status endpoint, see the Tasks section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTaskStatusRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetTaskStatusResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTaskStatusResult> GetTaskStatusAsync(SPGetTaskStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskStatusResult, SPTaskStatusResponseDataList>("/v1/client/tasks/get-status", AuthType, request);
            return result;
        }
    }
}