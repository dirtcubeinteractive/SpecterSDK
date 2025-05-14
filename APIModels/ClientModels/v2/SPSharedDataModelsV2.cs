using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPMatchResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    #region App

    [Serializable]
    public class SPPricingCurrencyData : ISpecterResourceData, ISpecterPricingCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
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
    public class SPPriceDataV2
    {
        public SPPriceTypes priceType { get; set; }
        public string productId { get; set; }
        public long price { get; set; }
        public float? discount { get; set; }
        public float? bonusCashAllowance { get; set; }
        public SPPricingCurrencyData currencyDetails { get; set; }
        public SPRealWorldCurrencyData realWorldCurrency { get; set; }
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
    
    #endregion
}