using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyTaskProgressResponse : ISpecterApiResponseData
    {
        public List<SPTaskProgressData> taskProgresses { get; set; }
        public int totalCount { get; set; }
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