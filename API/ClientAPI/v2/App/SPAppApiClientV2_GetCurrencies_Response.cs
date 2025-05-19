using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetCurrenciesResponse : ISpecterApiResponseData
    {
        public List<SPCurrencyData> currencies { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPCurrencyData : ISpecterResourceData, ISpecterCurrencyData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public string code { get; set; }
        public SPCurrencyTypeV2 type { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}