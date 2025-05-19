using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    public interface IAchievementsResourceData { }
    public class SPTaskResourceData : SPResourceResponseData, IAchievementsResourceData { }
    public class SPTaskGroupResourceData : SPResourceResponseData, IAchievementsResourceData { }

    [Serializable]
    public class SPEvent
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class SPTaskData : IAchievementsResourceData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public int? sortingOrder { get; set; }
        public SPEvent @event { get; set; }
        public List<SPRuleData> businessLogic { get; set; }
        public SPRewardsData rewardDetails { get; set; }
        public SPRewardsData linkedRewardDetails { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}