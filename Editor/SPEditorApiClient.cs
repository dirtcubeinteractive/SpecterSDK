using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIClients;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.Editor
{
    [Serializable]
    public class SPAppEvent
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<SPAppEventParameter> customParameterDetails { get; set; }
    }

    [Serializable]
    public class SPAppEventParameter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int dataTypeId { get; set; }
    }

    [Serializable]
    public class SPTaskAdminModel
    {
        public string id { get; set; }
        public string taskId { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
        
        public List<SPTaskReward> taskRewards { get; set; }
    }

    [Serializable]
    public class SPTaskReward
    {
        public string id { get; set; }
        public string itemId { get; set; }
        public string currencyId { get; set; }
        public string bundleId { get; set; }
        public string progressionMarkerId { get; set; }
        public int quantity { get; set; }
    }

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPTaskRewardConfig
    {
        public int? progressionMarkerId { get; set; }
        public int? currencyId { get; set; }
        public string itemId { get; set; }
        public string bundleId { get; set; }
        
        public int quantity { get; set; }
    }

    [Serializable]
    public class SPGetCustomEventsAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; }
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    [Serializable]
    public class SPGetCustomEventsAdminResponseData
    {
        public List<SPAppEvent> appEventDetails { get; set; }
    }

    [Serializable]
    public class SPGetTaskListAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; } = new();
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    [Serializable]
    public class SPGetTaskListAdminResponseData : List<SPTaskAdminModel> {  }

    [Serializable]
    public class SPCreateTaskAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public string name { get; set; }
        public string taskId { get; set; }
        public string eventId { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string rewardClaim { get; set; }
    }

    public class SPEditorApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPEditorApiClient(SpecterRuntimeConfig config) : base(config) {  }

        public async Task<SPGetCustomEventsAdminResponseData> GetCustomEvents(SPGetCustomEventsAdminRequest request)
        {
            ConfigureProjectId(request);
            
            var response = await PostAsync<SPGetCustomEventsAdminResponseData>("/v1/app-event/get/custom", AuthType, request);
            return response.data;
        }

        public async Task<SPGetTaskListAdminResponseData> GetTaskList(SPGetTaskListAdminRequest request)
        {
            ConfigureProjectId(request);

            var response = await PostAsync<SPGetTaskListAdminResponseData>("/v1/task/get", AuthType, request);
            return response.data;
        }
    }
}