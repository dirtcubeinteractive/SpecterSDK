using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    public sealed class SPCurrencyType : SPEnum<SPCurrencyType>
    {
        public static readonly SPCurrencyType Real = new SPCurrencyType(0, nameof(Real).ToLower(), nameof(Real));
        public static readonly SPCurrencyType Virtual = new SPCurrencyType(1, nameof(Virtual).ToLower(), nameof(Virtual));

        private SPCurrencyType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

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
        public SPCurrencyType type { get; set; }

    }

    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }


    public class SPStoreCurrencyResponseData : SPCurrencyResponseData
    {
        public int quantity { get; set; }
        public List<SPPriceResponseData> prices { get; set; }
    }

    [Serializable]
    public class SPCurrencyResponseDataList : SPResponseDataList<SPCurrencyResponseData> { }

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
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public bool isConsumable { get; set; }
        public bool isEquippable { get; set; }
        public bool isTradable { get; set; }
        public bool isStackable { get; set; }
        public int? maxNumberOfStack { get; set; }
        public bool isRentable { get; set; }
    }


    // Item master data in SDK responses
    [Serializable]
    public class SPItemResponseData : SPItemResponseBaseData
    {
        public int? quantity { get; set; }
        public bool isLocked { get; set; }
        public int? consumeByCount { get; set; }
        public int? consumeByTime { get; set; }
    }

    public class SPItemResponseExtendedData : SPItemResponseData, ISpecterMasterData
    {
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }

    }

    public class SPItemPriceResponseData : SPItemResponseExtendedData
    {
        public List<SPPriceResponseData> prices { get; set; }
    }

    [Serializable]
    public class SPBundleResponseData : SPItemResponseBaseData
    {
        public int? quantity { get; set; }
        public bool isLocked { get; set; }
        public bool? isManual { get; set; }
        public int? consumeByCount { get; set; }
        public int? consumeByTime { get; set; }

    }

    public class SPBundleResponseExtendedData : SPBundleResponseData, ISpecterMasterData
    {
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }


    public class SPBundlePriceResponseData : SPBundleResponseExtendedData
    {
        public List<SPPriceResponseData> prices { get; set; }
    }

    public class SPBundleContentResponseData : SPBundlePriceResponseData
    {
        public SPBundleContent Contents { get; set; }
    }

    public class SPBundleContent
    {
        public List<SPItemResponseData> Items { get; set; }
        public List<SPBundleResponseData> Bundles { get; set; }
        public List<SPCurrencyResponseBaseData> Currencies { get; set; }
    }

    [Serializable]
    public class SPItemResponseDataList : SPResponseDataList<SPItemPriceResponseData> { }

    [Serializable]
    public class SPBundleResponseDataList : SPResponseDataList<SPBundleContentResponseData> { }


    // Store item data in SDK responses
    [Serializable]
    public class SPStoreItemResponseData : SPItemResponseData
    {

    }

    // User inventory item data in SDK responses
    [Serializable]
    public class SPInventoryItemResponseData : SPItemResponseBaseData
    {
        public string collectionId { get; set; }
        public int amount { get; set; }
    }

    [Serializable]
    public class SPInventoryBundleResponseData : SPItemResponseBaseData
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

    #endregion

    #region Api Models

    public class SPGetUserInventoryResponseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items;
        public List<SPInventoryBundleResponseData> bundles;
    }


    #endregion
}