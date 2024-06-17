using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.AdminModels
{
    [Serializable]
    public class SPTaskAdminData : ISpecterApiResponseData
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
    public class SPCreateTaskAdminRequest : SPApiRequestBase
    {
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
        public SPRewardGrantType rewardGrantType;
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
            rewardGrantType = SPRewardGrantType.Client;
            isLockedByLevel = false;
            isRecurring = false;
            rewardDetails = new List<SPTaskRewardConfig>();
            levelDetails = new List<SPProgressionAccessControlConfig>();
            tags = new List<string>();
            meta = new Dictionary<string, string>();
        }
    }
}