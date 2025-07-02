using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetCurrenciesResponse : ISpecterMasterResponse
    {
        public List<SPCurrencyData> currencies { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPCurrencyData : ISpecterResourceData, ISpecterEconomyResourceData, ISpecterCurrencyData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPRarityData rarity { get; set; }
        
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}