using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyTaskGroupStatusResponse : List<SPTaskGroupStatusData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPTaskGroupStatusData : ISpecterTaskGroupInfoData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPTaskGroupType taskGroupType { get; set; }
        
        public string instanceId { get; set; }
        public SPTaskGroupStatus status { get; set; }
        
        public List<SPTaskStatusInfoData> tasks { get; set; }
    }

    [Serializable]
    public class SPTaskStatusInfoData : ISpecterTaskStatusData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPTaskStatus status { get; set; }
        public string instanceId { get; set; }
    }
}