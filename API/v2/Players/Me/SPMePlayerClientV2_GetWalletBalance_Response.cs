using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyWalletBalanceResponse : List<SPWalletCurrencyData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPWalletCurrencyData : ISpecterPlayerOwnedEntityData, ISpecterCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPRarityData rarity { get; set; }
        
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
        
        public double balance { get; set; }
    }
}