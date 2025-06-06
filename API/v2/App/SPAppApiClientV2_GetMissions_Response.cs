using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetMissionsResponse : ISpecterMasterResponse
    {
        public List<SPMissionData> missions { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPMissionData : ISpecterResourceData, ISpecterTaskGroupData, ISpecterMasterData, ISpecterUnlockableData, ISpecterLiveOpsEntityData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPScheduleData schedule { get; set; }
        
        public SPTaskGroupType taskGroupType { get; set; }
        public List<SPTaskResourceData> tasks { get; set; }
        
        public SPRewardsData rewardDetails { get; set; }
        public SPRewardsData linkedRewardDetails { get; set; }
        
        public SPUnlockConditionsData unlockConditions { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}