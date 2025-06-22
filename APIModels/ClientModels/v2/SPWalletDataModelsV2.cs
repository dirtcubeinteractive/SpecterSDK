using System;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
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
    
    [Serializable]
    public class SPWalletHistoryEntryData : ISpecterTransactionData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public SPTransactionStatus status { get; set; }
        
        public SPTransactedCurrencyData currencyDetails { get; set; }
        
        public SPTransactedResourceData purchasedItem { get; set; }
        public SPTransactedResourceData purchasedBundle { get; set; }
        
        public SPTransactionPurposeData purpose { get; set; }
        public double amount { get; set; }
        public string remarks { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    [Serializable]
    public class SPTransactedCurrencyData : ISpecterCurrencyData
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

    [Serializable]
    public class SPTransactedResourceData : ISpecterEconomyResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
    }
}