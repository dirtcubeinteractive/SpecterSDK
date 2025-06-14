using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    public class SPGetTasksResponse : ISpecterMasterResponse
    {
        public List<SPTaskData> tasks { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPTaskData : ISpecterResourceData, ISpecterMasterData, ISpecterLiveOpsEntityData, ISpecterUnlockableData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPScheduleData schedule { get; set; }
        
        public int? sortingOrder { get; set; }
        public SPTaskGroupResourceData taskGroupDetails { get; set; }
        public SPEventData @event { get; set; }
        public SPBusinessLogicData businessLogic { get; set; }
        public List<SPRuleParamData> parameters { get; set; }
        
        public SPRewardsData rewardDetails { get; set; }
        public SPRewardsData linkedRewardDetails { get; set; }

        public SPUnlockConditionsData unlockConditions { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}