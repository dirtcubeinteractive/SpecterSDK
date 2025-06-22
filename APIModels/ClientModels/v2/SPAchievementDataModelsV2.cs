using System;
using System.Collections.Generic;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPTaskStatusInfoData : ISpecterTaskStatusData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public string instanceId { get; set; }
        public SPTaskStatus status { get; set; }
        
        public SPTaskGroupResourceData taskGroupDetails { get; set; }
    }

    [Serializable]
    public class SPTaskGroupStatusInfoData : ISpecterTaskGroupResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPTaskGroupType taskGroupType { get; set; }

        public string instanceId { get; set; }
        public SPTaskGroupStatus status { get; set; }
        public List<SPTaskStatusInfoBaseData> tasks { get; set; }
    }

    [Serializable]
    public class SPTaskStatusInfoBaseData : ISpecterTaskStatusData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }

        public SPTaskStatus status { get; set; }
        public string instanceId { get; set; }
    }

    [Serializable]
    public class SPTaskProgressData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPEventData @event { get; set; }
        public List<SPParamProgressData> progress { get; set; }

        public string taskGroupId { get; set; }
    }
}