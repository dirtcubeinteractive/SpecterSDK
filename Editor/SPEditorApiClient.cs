using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecterSDK.APIClients;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.Editor
{
    [Serializable]
    public class SPAppEvent
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<SPAppEventParameter> defaultParameterDetails { get; set; }
        public List<SPAppEventParameter> customParameterDetails { get; set; }
        
        [JsonIgnore]
        public List<SPAppEventParameter> allParameters { get; private set; }
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
        public string name { get; set; }
        public string taskId { get; set; }
        public string defaultEventId { get; set; }
        public string customEventId { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string rewardClaim { get; set; }
        public bool isLockedByLevel { get; set; }
        public bool isRecurring { get; set; }
        public List<SPTaskRewardConfig> rewardDetails { get; set; }
        public List<object> levelDetails { get; set; }
        public List<string> tags { get; set; }
        public List<Dictionary<string, object>> config { get; set; }
        public Dictionary<string, object> businessLogic { get; set; }
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
    }
}