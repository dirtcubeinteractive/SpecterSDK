using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIClients;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;
using Newtonsoft.Json;

namespace SpecterSDK.Editor
{
    public enum SPAppEventType
    {
        Default,
        Custom
    }
    
    [Serializable]
    public class SPAppEvent
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<SPAppEventParameter> defaultParameterDetails { get; set; }
        public List<SPAppEventParameter> customParameterDetails { get; set; }
        
        [JsonIgnore] public string type { get; set; }
        [JsonIgnore] public List<SPAppEventParameter> allParameters { get; private set; }
        public List<SPAppEventParameter> GetAllParameters()
        {
            if (allParameters == null)
            {
                allParameters = new List<SPAppEventParameter>(defaultParameterDetails ?? new List<SPAppEventParameter>());
                if (customParameterDetails != null && customParameterDetails.Count > 0)
                    allParameters.AddRange(customParameterDetails);
            }
            
            return allParameters;
        }
    }

    public enum SPParamDataType
    {
        String = 1,
        Integer,
        Boolean,
        Float,
        DateTime,
        Logical
    }

    [Serializable]
    public class SPAppEventParameter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public SPParamDataType dataTypeId { get; set; }
    }

    public enum SPTaskType
    {
        Static,
        Daily,
        Weekly
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

    public enum SPRewardType
    {
        ProgressionMarker,
        Currency,
        Item,
        Bundle
    }
    
    [Serializable, Newtonsoft.Json.JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPTaskRewardConfig
    {
        public int? progressionMarkerId;
        public int? currencyId;
        public string itemId;
        public string bundleId;
        
        public int quantity { get; set; }

        [JsonIgnore]
        public SPRewardType type;
    }

    [Serializable]
    public abstract class SPGetAppEventsAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; }
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    [Serializable]
    public abstract class SPGetAppEventsAdminResponseData
    {
        public List<SPAppEvent> appEventDetails { get; set; }
    }
    
    [Serializable]
    public class SPGetDefaultEventsAdminRequest : SPGetAppEventsAdminRequest { }
    
    [Serializable]
    public class SPGetDefaultEventsAdminResponseData : SPGetAppEventsAdminResponseData { }

    [Serializable]
    public class SPGetCustomEventsAdminRequest : SPGetAppEventsAdminRequest { }

    [Serializable]
    public class SPGetCustomEventsAdminResponseData : SPGetAppEventsAdminResponseData { }

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
        [JsonRequired]
        public string name;
        [JsonRequired]
        public string taskId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string defaultEventId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string customEventId;

        [JsonRequired] 
        public string iconUrl;
        public string description;
        [JsonRequired]
        public string type;
        [JsonRequired]
        public string rewardClaim;
        [JsonRequired]
        public bool isLockedByLevel;
        [JsonRequired]
        public bool isRecurring;
        [JsonRequired]
        public List<SPTaskRewardConfig> rewardDetails;
        [JsonRequired]
        public List<object> levelDetails;
        [JsonRequired]
        public List<string> tags;
        public List<Dictionary<string, object>> config { get; set; }
        public Dictionary<string, object> businessLogic { get; set; }
        
        
        [Newtonsoft.Json.JsonIgnore] public string eventId;

        public SPCreateTaskAdminRequest()
        {
            iconUrl = "task-icon.png";
            type = "static";
            rewardClaim = "on-claim";
            isLockedByLevel = false;
            isRecurring = false;
            rewardDetails = new List<SPTaskRewardConfig>();
            levelDetails = new List<object>();
            tags = new List<string>();
        }
    }

    public class SPEditorApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPEditorApiClient(SpecterRuntimeConfig config) : base(config) {  }

        public async Task<SPGetDefaultEventsAdminResponseData> GetDefaultEvents(SPGetDefaultEventsAdminRequest request)
        {
            ConfigureProjectId(request);

            var response = await PostAsync<SPGetDefaultEventsAdminResponseData>("/v1/app-event/get/default", AuthType, request);
            return response.data;
        }
        
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

        public async Task<Dictionary<string, object>> CreateTask(SPCreateTaskAdminRequest request)
        {
            ConfigureProjectId(request);

            var response = await PostAsync<Dictionary<string, object>>("/v1/task/create", AuthType, request);
            return response.data;
        }
    }
}