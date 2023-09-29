using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIClients;
using SpecterSDK.APIDataModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;
using Newtonsoft.Json;
using SpecterSDK.APIDataModels;

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

    [Serializable]
    public class SPTaskAdminModel : ISpecterApiResponseData
    {
        public string id;
        public string taskId;
        public string name;
        public string description;
        public string iconUrl;
        public bool isLockedByLevel;
        public bool isRecurring;
        public List<Dictionary<string, object>> config;
        public Dictionary<string, object> businessLogic;
        public List<object> currencies;
        public List<object> bundles;
        public List<object> items;
        public List<object> progressionMarkers;
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
        public int? progressionMarkerId;
        public int? currencyId;
        public string itemId;
        public string bundleId;
        
        public int quantity { get; set; }

        [JsonIgnore]
        public SPRewardType type;
    }

    [Serializable]
    public class SPProgressionAccessControlConfig
    {
        [JsonRequired]
        public string levelSystemId;
        [JsonRequired]
        public int level;

        [JsonIgnore] 
        public int selectedSystemIndex;
    }

    public class SPMetaConfig
    {
        public string key;
        public string value;
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
    public class SPGetAppEventsAdminResponseData : ISpecterApiResponseData
    {
        public List<SPAppEvent> appEventDetails { get; set; }
    }
    
    [Serializable]
    public class SPGetDefaultEventsAdminRequest : SPGetAppEventsAdminRequest { }

    public class SPGetDefaultEventsAdminResult : SpecterApiResultBase<SPGetAppEventsAdminResponseData>
    {
        public List<SPAppEvent> AppEventDetails;

        protected override void InitSpecterObjectsInternal()
        {
            AppEventDetails = Response.data.appEventDetails;
        }
    }

    [Serializable]
    public class SPGetCustomEventsAdminRequest : SPGetAppEventsAdminRequest { }

    public class SPGetCustomEventsAdminResult : SpecterApiResultBase<SPGetAppEventsAdminResponseData>
    {
        public List<SPAppEvent> AppEventDetails;

        protected override void InitSpecterObjectsInternal()
        {
            AppEventDetails = Response.data.appEventDetails;
        }
    }

    [Serializable]
    public class SPGetProgressionSystemsAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; }
    }

    [Serializable]
    public class SPGetProgressionSystemsAdminResponseData : ISpecterApiResponseData
    {
        public List<SPProgressionSystemAdminModel> levelDetails;
    }

    public class SPGetProgressionSystemsAdminResult : SpecterApiResultBase<SPGetProgressionSystemsAdminResponseData>
    {
        public List<SPProgressionSystemAdminModel> LevelDetails;

        protected override void InitSpecterObjectsInternal()
        {
            LevelDetails = Response.data.levelDetails;
        }
    }

    [Serializable]
    public class SPProgressionSystemAdminModel
    {
        public string id;
        public string levelSystemId;
        public string name;
        public int progressionMarkerId;
        public SPProgressionMarkerAdminModel progressionMarker;
        public List<SPProgressionSystemLevelAdminModel> levelSystemLevelMapping;
    }

    [Serializable]
    public class SPProgressionSystemLevelAdminModel
    {
        public string id;
        public int levelNo;
        public int parameterValue;
    }

    [Serializable]
    public class SPProgressionMarkerAdminModel
    {
        public int id;
        public string progressionMarkerId;
        public string name;
    }

    [Serializable]
    public class SPGetTaskListAdminRequest : IProjectConfigurable
    {
        public string projectId { get; set; }
        public List<string> ids { get; set; } = new();
        public int limit { get; set; } = 50;
        public int offset { get; set; }
    }

    public class SPGetTaskListAdminResult : SpecterApiResultBase<SPResponseDataList<SPTaskAdminModel>>
    {
        public List<SPTaskAdminModel> TaskList;

        protected override void InitSpecterObjectsInternal()
        {
            TaskList = Response.data;
        }
    }

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
        public SPTaskType type;
        [JsonRequired]
        public SPRewardClaimType rewardClaim;
        [JsonRequired]
        public bool isLockedByLevel;
        [JsonRequired]
        public bool isRecurring;
        [JsonRequired]
        public List<SPTaskRewardConfig> rewardDetails;
        [JsonRequired]
        public List<SPProgressionAccessControlConfig> levelDetails;
        [JsonRequired]
        public List<string> tags;
        public Dictionary<string, string> meta;
        public List<Dictionary<string, object>> config { get; set; }
        public Dictionary<string, object> businessLogic { get; set; }


        [JsonIgnore] 
        public string eventId;

        public SPCreateTaskAdminRequest()
        {
            iconUrl = "task-icon.png";
            type = SPTaskType.Static;
            rewardClaim =  SPRewardClaimType.Automatic;
            isLockedByLevel = false;
            isRecurring = false;
            rewardDetails = new List<SPTaskRewardConfig>();
            levelDetails = new List<SPProgressionAccessControlConfig>();
            tags = new List<string>();
            meta = new Dictionary<string, string>();
        }
    }

    public class SPEditorApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPEditorApiClient(SpecterRuntimeConfig config) : base(config) {  }

        public async Task<SPGetDefaultEventsAdminResult> GetDefaultEvents(SPGetDefaultEventsAdminRequest request)
        {
            ConfigureProjectId(request);

            var result = await PostAsync<SPGetDefaultEventsAdminResult, SPGetAppEventsAdminResponseData>("/v1/app-event/get/default", AuthType, request);
            return result;
        }
        
        public async Task<SPGetCustomEventsAdminResult> GetCustomEvents(SPGetCustomEventsAdminRequest request)
        {
            ConfigureProjectId(request);
            Debug.Log(request.projectId);
            var result = await PostAsync<SPGetCustomEventsAdminResult, SPGetAppEventsAdminResponseData>("/v1/app-event/get/custom", AuthType, request);
            return result;
        }

        public async Task<List<SPAppEvent>> GetEvents()
        {
            var ConstructEvents = new Func<SPAppEventType, List<SPAppEvent>, List<SPAppEvent>, List<SPAppEvent>>((type, unionEvents, events) =>
            {
                foreach (var appEvent in events)
                    appEvent.type = type.ToString().ToLower();
                unionEvents.AddRange(events);
                return unionEvents;
            });

            var customEventsResult = await GetCustomEvents(new SPGetCustomEventsAdminRequest());
            var defaultEventsResult = await GetDefaultEvents(new SPGetDefaultEventsAdminRequest());

            if (customEventsResult == null || defaultEventsResult == null)
                return new List<SPAppEvent>();
                
            var allEvents = new List<SPAppEvent>();
            allEvents = ConstructEvents(SPAppEventType.Custom, allEvents, customEventsResult.AppEventDetails);
            allEvents = ConstructEvents(SPAppEventType.Default, allEvents, defaultEventsResult.AppEventDetails);

            return allEvents;
        }

        public async Task<SPGetTaskListAdminResult> GetTaskList(SPGetTaskListAdminRequest request)
        {
            ConfigureProjectId(request);
            Debug.Log(request.projectId);
            var result = await PostAsync<SPGetTaskListAdminResult, SPResponseDataList<SPTaskAdminModel>>("/v1/task/get", AuthType, request);
            return result;
        }

        public async Task<SPGeneralResult> CreateTask(SPCreateTaskAdminRequest request)
        {
            ConfigureProjectId(request);

            var result = await PostAsync<SPGeneralResult, SPGeneralResponseDictionaryData>("/v1/task/create", AuthType, request);
            return result;
        }

        public async Task<SPGetProgressionSystemsAdminResult> GetProgressionSystems(SPGetProgressionSystemsAdminRequest request)
        {
            ConfigureProjectId(request);

            var result = await PostAsync<SPGetProgressionSystemsAdminResult, SPGetProgressionSystemsAdminResponseData>("/v1/level-system/get", AuthType, request);
            return result;
        }
    }
}