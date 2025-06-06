using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v1.App
{
    /// <summary>
    /// Represents a request to get task groups from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetTaskGroupsRequest class represents a request to get task groups from the Specter App API.
    /// It can be used to specify the filter criteria for the task groups to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetTaskGroupsAsync method in the SPAppApiClient class to retrieve the task groups from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTaskGroupsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of the task group types as a filter criteria for retrieving task groups from the Specter App API.
        /// <remarks>
        /// <para>
        /// The taskGroupTypes property is used in the SPGetTaskGroupsRequest class to specify the specific task group types for filtering the retrieved task groups from the API.
        /// See <see cref="SPTaskGroupType"/> for available options.
        /// </para>
        /// </remarks>
        /// </summary>
        public List<SPTaskGroupType> taskGroupTypes { get; set; }
        
        /// <summary>
        /// Represents a list of task group IDs used as filter criteria for retrieving task groups from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The task groupIds property is used in the SPGetTaskGroupsRequest class to specify the specific task group IDs for filtering the retrieved task groups from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique task group ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// A flag to include tasks that are inactive for the current time period cycle (e.g. in Daily Missions, fetch even tasks that
        /// are not included for the current day)
        /// </summary>
        public bool includeInactiveTasks { get; set; }
        
        /// <summary>
        /// A filter to fetch task groups based on the status of their schedule. See <see cref="SPScheduleStates"/> for possible values.
        /// </summary>
        public List<SPScheduleStates> scheduleStatuses { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetTaskGroupsAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetTaskGroupsResult: SpecterApiResultBase<SPGetTaskGroupsResponseData>
    {
        // List of all task groups fetched
        public List<SpecterTaskGroup> TaskGroups;
        
        // Total count of all task groups configured on the dashboard
        public int TotalTaskGroupCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            TaskGroups = new List<SpecterTaskGroup>();
            foreach (var taskGroup in Response.data.taskGroups)
            {
                TaskGroups.Add(new SpecterTaskGroup(taskGroup)); 
            }
            TotalTaskGroupCount = Response.data.totalCount;
        }
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of task groups asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetTaskGroupsRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetTaskGroupsResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetTaskGroupsResult> GetTaskGroupsAsync(SPGetTaskGroupsRequest request)
        {
            var result = await PostAsync<SPGetTaskGroupsResult, SPGetTaskGroupsResponseData>("/v1/client/app/get-task-groups", AuthType, request);
            return result;
        }
    }
}