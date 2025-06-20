using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyWalletHistoryResponse : List<SPWalletTransactionData> ,ISpecterApiResponseData
    {
    }

    [Serializable]
    public class SPWalletTransactionData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        
        public SPWalletTransactionCurrencyData currencyDetails { get; set; }
        
        public SPWalletTransactionResourceData purchasedItem { get; set; }
        public SPWalletTransactionResourceData purchasedBundle { get; set; }
        
        public SPTransactionPurposeData purpose { get; set; }
        public double amount { get; set; }
        public string remarks { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    [Serializable]
    public class SPWalletTransactionResourceData : ISpecterEconomyResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
    }

    [Serializable]
    public class SPWalletTransactionCurrencyData : ISpecterCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPRarityData rarity { get; set; }
        
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
    }
}