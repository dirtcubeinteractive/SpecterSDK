using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Tasks
{
    /// <summary>
    /// Represents a request to force complete one or more tasks in Specter SDK.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPForceCompleteTaskRequest : SPApiRequestBase
    {
        /// <summary>
        /// A list of dashboard specific IDs of the tasks to force complete. Currently limited to 1.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// A flag to indicate whether the server should provide details about any configured rewards for the task in the response.
        /// </summary>
        public bool getRewardDataInResponse { get; set; }
    }

    /// <summary>
    /// Represents the result of forcing the completion of a task in Specter.
    /// </summary>
    public class SPForceCompleteTaskResult : SpecterApiResultBase<SPForceCompleteTaskResponseDataList>
    {
        /// <summary>
        /// List of objects containing details about the tasks that were force completed.
        /// Currently limited to 1 since the request is limited to 1 task ID.
        /// </summary>
        public List<SpecterForceCompletedTaskInfo> ForceCompletedTaskInfos;
        
        protected override void InitSpecterObjectsInternal()
        {
            ForceCompletedTaskInfos = new List<SpecterForceCompletedTaskInfo>();
            foreach (var taskData in Response.data)
            {
                ForceCompletedTaskInfos.Add(new SpecterForceCompletedTaskInfo(taskData));
            }
        }
    }

    public partial class SPTasksApiClient
    {
        /// <summary>
        /// Force complete a task asynchronously.
        /// <remarks>
        /// <para>
        /// Force completing a task means to bypass any rules and events configured for the task on the dashboard.
        /// Specter assumes that you have handled task parameters on your end and simply marks the task as completed for the
        /// user.
        /// </para>
        /// <para>
        /// Any rewards configured for the task are also added to the user's reward history for server or client side processing. If a task
        /// within a task group like step-series is force completed, it will also update the status of the task group.
        /// </para>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPForceCompleteTaskRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPForceCompleteTaskResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPForceCompleteTaskResult> ForceCompleteTaskAsync(SPForceCompleteTaskRequest request)
        {
            var result = await PostAsync<SPForceCompleteTaskResult, SPForceCompleteTaskResponseDataList>("/v1/client/tasks/force-complete", AuthType, request);
            return result;
        }
    }
}