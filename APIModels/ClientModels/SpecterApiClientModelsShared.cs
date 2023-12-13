using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public abstract class SPResourceResponseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class SPApiEventConfigurableRequestBase : SPApiRequestBase, ISpecterEventConfigurable
    {
        public Dictionary<string, object> customParams { get; set; }
        public Dictionary<string, object> systemParams { get; set; }
    }
    
    [Serializable]
    public class SPGetServerTimeResponseData : ISpecterApiResponseData
    {
        public string abbreviation { get; set; }
        public string clientIp { get; set; }
        public DateTime datetime { get; set; }
        public int dayOfWeek { get; set; }
        public int dayOfYear { get; set; }
        public bool dst { get; set; }
        public string dstFrom { get; set; }
        public int dstOffset { get; set; }
        public string dstUntil { get; set; }
        public int rawOffset { get; set; }
        public string timezone { get; set; }
        public int unixtime { get; set; }
        public DateTime utcDatetime { get; set; }
        public string utcOffset { get; set; }
        public int weekNumber { get; set; }
    }

    [Serializable]
    public class SPUnlockConditionResponseData
    {
        public int? lockedLevelNo { get; set; }
        public SPUnlockResourceData unlockItem { get; set; }
        public SPUnlockResourceData unlockBundle { get; set; }
        public SPUnlockResourceData unlockProgressionSystem { get; set; }
    }

    [Serializable]
    public class SPUnlockResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}