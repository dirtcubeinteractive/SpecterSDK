using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    #region App

    [Serializable]
    public class SPGameResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
    }
    
    [Serializable]
    public class SPMatchResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    [Serializable]
    public class SPLeaderboardResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    [Serializable]
    public class SPCompetitionResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    [Serializable]
    public class SPProgressionMarkerResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    [Serializable]
    public class SPProgressionSystemResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    [Serializable]
    public class SPTaskResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPEventData @event { get; set; }
        
        public SPRewardsData rewardDetails { get; set; }
        public SPRewardsData linkedRewardDetails { get; set; }
    }

    [Serializable]
    public class SPTaskGroupResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    [Serializable]
    public class SPPricingCurrencyData : ISpecterResourceData, ISpecterEconomyResourceData, ISpecterPricingCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
        public string code { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    public class SPRealWorldCurrencyData : ISpecterPricingCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string countryName { get; set; }
    }

    [Serializable]
    public class SPPriceDataV2 : ISpecterPriceData
    {
        public string productId { get; set; }
        
        public SPPriceTypes priceType { get; set; }
        public long price { get; set; }
        public float? discount { get; set; }
        public float? bonusCashAllowance { get; set; }
        
        public SPPricingCurrencyData currencyDetails { get; set; }
        public SPRealWorldCurrencyData realWorldCurrency { get; set; }
    }

    [Serializable]
    public class SPEntryFeeDataV2 : ISpecterPriceData
    {
        public SPPriceTypes priceType { get; set; }
        public long price { get; set; }
        public float? discount { get; set; }
        public float? bonusCashAllowance { get; set; }
        
        public SPPricingCurrencyData currencyDetails { get; set; }
        public SPRealWorldCurrencyData realWorldCurrency { get; set; }
        
        public double? hostingFee { get; set; }
        public SPHostingFeeTypes hostingFeeType { get; set; }
    }

    [Serializable]
    public class SPUnlockConditionsData
    {
        public List<SPUnlockResourceData> unlockItem { get; set; }
        public List<SPUnlockResourceData> unlockBundle { get; set; }
        public List<SPUnlockLevelData> unlockLevel { get; set; }
    }

    [Serializable]
    public class SPUnlockLevelData
    {
        public int lockedLevelNo  { get; set; }
        public SPUnlockResourceData unlockProgressionSystem { get; set; }
    }

    [Serializable]
    public class SPRarityData
    {
        public SPRarity id { get; set; }
        public string name { get; set; }
    }
    
    #endregion
}