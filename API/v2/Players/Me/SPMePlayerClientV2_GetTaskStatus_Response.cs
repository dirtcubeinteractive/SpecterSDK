using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyTaskStatusResponse : List<SPTaskStatusInfoData>, ISpecterApiResponseData
    {
    }

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
}