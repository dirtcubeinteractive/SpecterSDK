using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents a request to get tasks from the Specter App API.
    /// These tasks are any tasks created OUTSIDE of task groups.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetTasksRequest class represents a request to get tasks from the Specter App API.
    /// It can be used to specify the filter criteria for the tasks to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTasksAsync method in the SPAppApiClient class to retrieve the tasks from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTasksRequest : SPPaginatedApiRequest, ITagFilterable
    {
        /// <summary>
        /// Represents a list of task IDs used as filter criteria for retrieving tasks from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The taskIds property is used in the SPGetTasksRequest class to specify the specific task IDs for filtering the retrieved tasks from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique task ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// A flag to fetch even tasks that are part of task groups in this api call
        /// </summary>
        public bool includeTaskGroupTasks { get; set; }
        
        /// <summary>
        /// Additional attributes of the retrieved tasks that you wish to receive (eg: createdAt, updatedAt etc.)
        /// </summary>
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// Additional entities that can be fetched along with the list of tasks
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTasksAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetTasksResult : SpecterApiResultBase<SPGetTasksResponseData>
    {
        // List of all tasks fetched.
        public List<SpecterTask> Tasks;
        
        // Total count of all tasks outside of task groups configured on the dashboard.
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
        /// <summary>
        /// Get the list of Specter tasks asynchronously. The tasks retrieved by this endpoint are any standalone tasks created on your dashboard that are not part of a task group.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTasksRequest"/>.
        /// </param>
        /// <returns>
        /// A System.Task representing the asynchronous operation. The task result contains the <see cref="SPGetTasksResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTasksResult> GetTasksAsync(SPGetTasksRequest request)
        {
            var result = await PostAsync<SPGetTasksResult, SPGetTasksResponseData>("/v1/client/app/get-tasks", AuthType, request);
            return result;
        }
    }
}