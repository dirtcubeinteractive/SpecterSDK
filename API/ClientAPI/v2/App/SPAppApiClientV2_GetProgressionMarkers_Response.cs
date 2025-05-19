using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetProgressionMarkersResponse
    {
        public List<SPProgressionMarkerData> markers { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPProgressionMarkerData : ISpecterResourceData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public List<SPProgressionSystemResourceData> progressionSystems { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}