using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetStepSeriesResponse : ISpecterMasterResponse
    {
        public List<SPStepSeriesData> stepSeries { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPStepSeriesData : ISpecterResourceData, ISpecterTaskGroupData, ISpecterLiveOpsEntityData, ISpecterMasterData, ISpecterUnlockableData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPScheduleData schedule { get; set; }
        
        public SPTaskGroupType taskGroupType { get; set; }
        public List<SPTaskResourceData> tasks { get; set; }
        
        public SPUnlockConditionsData unlockConditions { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}