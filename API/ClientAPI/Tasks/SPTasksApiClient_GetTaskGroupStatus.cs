using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    /// <summary>
    /// Represents the request structure to get the user's completion and reward claim status across all task groups and their associated tasks
    /// as configured by you on the Specter dashboard.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetTaskGroupStatusRequest"/> class represents a request to get a user's task group statuses from the Specter Tasks API.
    /// It can be used to specify the filter criteria for the task groups for which statuses are to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTaskGroupStatusAsync method in the <see cref="SPTasksApiClient"/> class to retrieve task group status information for the user from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupStatusRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// A list of <see cref="SPTaskGroupType"/> to filter retrieval of task group statuses by their type.
        /// </summary>
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        
        /// <summary>
        /// A list of dashboard specific task group IDs to limit retrieval of task group statuses to specific task groups.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// Flag to fetch even inactive tasks within the current time period cycle
        /// </summary>
        public bool includeInactiveTasks { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTaskGroupStatusAsync method of the <see cref="SPTasksApiClient"/> class.
    /// </summary>
    public class SPGetTaskGroupStatusResult : SpecterApiResultBase<SPTaskGroupStatusResponseDataList>
    {
        // List of task group statuses fetched.
        public List<SpecterTaskGroupStatus> TaskGroupStatuses;
        
        protected override void InitSpecterObjectsInternal()
        {
            TaskGroupStatuses = new List<SpecterTaskGroupStatus>();
            foreach (var taskGroup in Response.data)
            {
                TaskGroupStatuses.Add(new SpecterTaskGroupStatus(taskGroup));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        /// <summary>
        /// Get the task group statuses for a user asynchronously.
        /// <remarks>
        /// For full information about the get task group status endpoint, see the Tasks section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTaskGroupStatusRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetTaskGroupStatusResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTaskGroupStatusResult> GetTaskGroupStatusAsync(SPGetTaskGroupStatusRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupStatusResult, SPTaskGroupStatusResponseDataList>("/v1/client/tasks/get-task-group-status", AuthType, request);
            return result;
        }
    }
}