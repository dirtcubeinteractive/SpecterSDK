using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetProgressionSystemsResponse : ISpecterMasterResponse
    {
        public List<SPProgressionSystemData> progressionSystems { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPProgressionSystemData : ISpecterResourceData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPProgressionMarkerResourceData progressionMarker { get; set; }
        public List<SPProgressionLevelData> levels { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
        
        public int totalLevels { get; set; }
    }

    [Serializable]
    public class SPProgressionLevelData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int levelNo { get; set; }
        public long incrementalParameterValue { get; set; }
        public long cumulativeParameterValue { get; set; }
        
        public SPRewardsData rewardDetails { get; set; }
    }
}