using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Currency Response Data Models

    [Serializable]
    public sealed class SPCurrencyType : SPEnum<SPCurrencyType>
    {
        public static readonly SPCurrencyType Real = new SPCurrencyType(0, nameof(Real).ToLower(), nameof(Real));
        public static readonly SPCurrencyType Virtual = new SPCurrencyType(1, nameof(Virtual).ToLower(), nameof(Virtual));

        private SPCurrencyType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public class SPCurrencyResponseBaseData : SPResourceResponseData
    {
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
    }

    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
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

    #endregion

    #region Item Response Data Models

    // Base for item and bundle master data in SDK responses
    [Serializable]
    public class SPCollectibleResourceResponseData : SPResourceResponseData, ISpecterMasterData
    {
        public bool isConsumable { get; set; }
        public bool isEquippable { get; set; }
        public bool isTradable { get; set; }
        public bool isStackable { get; set; }
        public int? stackCapacity { get; set; }
        public bool isRentable { get; set; }
        public bool isLocked { get; set; }
        public int? consumeByUses { get; set; }
        public int? consumeByTime { get; set; }
        public int? quantity { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<SPPriceData> prices { get; set; }
    }

    // Item master data in SDK responses
    [Serializable]
    public class SPItemResponseData : SPCollectibleResourceResponseData
    {
        public bool? isDefaultLoadout { get; set; }
    }

    [Serializable]
    public class SPItemResponseDataList : SPResponseDataList<SPItemResponseData> { }

    #endregion

    //Bundle data in SDK response

    #region Bundle Response Data Models

    [Serializable]
    public class SPBundleResponseData : SPCollectibleResourceResponseData
    {
        public bool? isManual { get; set; }
        public SPBundleContentsData contents { get; set; }

    }

    [Serializable]
    public class SPBundleContentsData
    {
        public List<SPBundleResourceData> items { get; set; }
        public List<SPBundleResourceData> bundles { get; set; }
        public List<SPBundleResourceData> currencies { get; set; }
    }

    [Serializable]
    public class SPBundleResourceData : SPResourceResponseData
    {
        public int quantity { get; set; }
    }

    [Serializable]
    public class SPBundleResponseDataList : SPResponseDataList<SPBundleResponseData> { }

    #endregion

    //Store item data in SDK responses

    #region Store Response Data Models

    [Serializable]
    public abstract class SPStoreResourceResponseData : SPResourceResponseData
    {
        // Quantity of resource available in store - for limited edition resources
        public int quantity { get; set; }
        public List<SPPriceData> prices { get; set; }
    }

    [Serializable]
    public class SPStoreItemResponseData : SPStoreResourceResponseData { }


    [Serializable]
    public class SPStoreBundleResponseData : SPStoreResourceResponseData { }


    [Serializable]
    public class SPStoreCurrencyResponseData : SPStoreResourceResponseData { }

    #endregion

    // User inventory item data in SDK responses

    #region Inventory Response Data Models

    [Serializable]
    public abstract class SPInventoryResourceResponseData : SPResourceResponseData
    {
        public string collectionId { get; set; }
        public int totalUsesAvailable { get; set; }
        public bool isEquipped { get; set; }
        public int quantity { get; set; }
    }

    [Serializable]
    public class SPInventoryItemResponseData : SPInventoryResourceResponseData
    {
    }

    [Serializable]
    public class SPInventoryBundleResponseData : SPInventoryResourceResponseData
    {
        public bool isManual { get; set; }
    }

    [Serializable]
    public class SPGetUserInventoryResponseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items { get; set; }
        public List<SPInventoryBundleResponseData> bundles { get; set; }
    }
    #endregion


    #region Price Response Data Models

    [Serializable]
    public class SPPriceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public SPPriceTypes priceType { get; set; }
        public double price { get; set; }
        public int discount { get; set; }
        public int bonusCashAllowance { get; set; }
        public int? gamePlatformMasterId { get; set; }

        // This is any currency configured on the Specter dashboard
        public SPCurrencyResponseBaseData virtualCurrency { get; set; }
        // This is an actual currency (eg: USD, INR, etc.)
        public SPRealWorldCurrencyResponseData realWorldCurrency { get; set; }
    }

    #endregion
}