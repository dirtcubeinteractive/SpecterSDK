using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Achievements
{
    public class SPForceCompleteTaskResponse : List<SPForceCompletedTaskData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPForceCompletedTaskData : ISpecterTaskResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPTaskGroupResourceData taskGroupDetails { get; set; }
        public SPRewardsData rewardDetails { get; set; }
    }
}