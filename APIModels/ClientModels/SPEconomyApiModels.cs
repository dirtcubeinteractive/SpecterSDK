using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Response Data Models

    // Base for currency data in SDK responses
    [Serializable]
    public abstract class SPCurrencyResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string iconUrl { get; set; }
        public string type { get; set; }

    }
    
    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
    
    // User wallet currency data in SDK responses
    [Serializable]
    public class SPWalletCurrencyResponseData : SPCurrencyResponseBaseData
    {
        public float balance { get; set; }
    }

    [Serializable]
    public class SPWalletCurrencyResponseDataList : SPResponseDataList<SPWalletCurrencyResponseData> { }

    // Base for item data in SDK responses
    [Serializable]
    public abstract class SPItemResponseBaseData : ISpecterApiResponseData
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public bool isConsumable { get; set; }
        public bool isEquippable { get; set; }
        public bool isTradable { get; set; }
        public bool isStackable { get; set; }
        public bool isRentable { get; set; }
        public int? maxNumberOfStack { get; set; }
    }

    // Item master data in SDK responses
    [Serializable]
    public class SPItemResponseData : SPItemResponseBaseData, ISpecterMasterData
    {
        public int? quantity { get; set; }
        public bool isLocked { get; set; }
        public int? consumeByCount { get; set; }
        public int? consumeByTime { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<SPPriceResponseData> prices { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
    
    [Serializable]
    public class SPItemResponseDataList : SPResponseDataList<SPItemResponseData> { }

    // Store item data in SDK responses
    [Serializable]
    public class SPStoreItemResponseData : SPItemResponseData
    {
        public string storeId { get; set; }
    }
    
    // User inventory item data in SDK responses
    [Serializable]
    public class SPInventoryItemResponseData : SPItemResponseBaseData
    {
        public string collectionId { get; set; }
        public int amount { get; set; }
    }
    
    [Serializable]
    public class SPPriceResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public SPPriceTypes priceType { get; set; }
        public double price { get; set; }
        public int discount { get; set; }
        public int bonusCashAllowance { get; set; }
        public int? gamePlatformMasterId { get; set; }
        
        // This is any currency configured on the Specter dashboard
        public SPCurrencyResponseData virtualCurrency { get; set; }
        // This is an actual currency (eg: USD, INR, etc.)
        public SPRealWorldCurrencyResponseData realWorldCurrency { get; set; }
    }
    
    [Serializable]
    public class SPRealWorldCurrencyResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string countryName { get; set; }
    }

    #endregion
}