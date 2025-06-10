using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.v2;
using SPPrizeDistributionData = SpecterSDK.APIModels.ClientModels.v2.SPPrizeDistributionData;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetLeaderboardsResponse : ISpecterMasterResponse
    {
        public List<SPLeaderboardData> leaderboards { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPLeaderboardData : ISpecterResourceData, ISpecterMasterData, ISpecterLiveOpsEntityData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPScheduleData schedule { get; set; }
        
        public SPMatchResourceData match { get; set; }
        public SPLeaderboardRankingMethodData rankingMethod { get; set; }
        public SPLeaderboardSourceData sourceType { get; set; }
        public SPPrizeDistributionData prizeDistribution { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPLeaderboardRankingMethodData
    {
        public SPRankingMethod id { get; set; }
        public string name { get; set; }
    }
}