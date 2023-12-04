using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{

    #region Currency Response Data Models

    public sealed class SPCurrencyType : SPEnum<SPCurrencyType>
    {
        public static readonly SPCurrencyType Real = new SPCurrencyType(0, nameof(Real).ToLower(), nameof(Real));
        public static readonly SPCurrencyType Virtual = new SPCurrencyType(1, nameof(Virtual).ToLower(), nameof(Virtual));

        private SPCurrencyType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }


    // Base for currency data in SDK responses
    [Serializable]
    public abstract class SPCurrencyResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    // Currency master data in SDK responses
    [Serializable]
    public class SPCurrencyResponseData : SPCurrencyResponseBaseData
    {
        public string code { get; set; }
        public SPCurrencyType type { get; set; }

    }

    public class SPCurrencyResponseExtendedData : SPCurrencyResponseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    public class SPCurrencyInfoResponseData : SPCurrencyResponseBaseData
    {
        public int quantity { get; set; }
    }

    [Serializable]
    public class SPCurrencyResponseDataList : SPResponseDataList<SPCurrencyResponseExtendedData> { }


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
    public class SPWalletCurrencyResponseData : SPCurrencyResponseData
    {
        public float balance { get; set; }
    }


    [Serializable]
    public class SPWalletCurrencyResponseDataList : SPResponseDataList<SPWalletCurrencyResponseData> { }




    #endregion

    // Base for item data in SDK responses

    #region Item Response Data Models
    [Serializable]
    public abstract class SPItemResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public int? quantity { get; set; }
    }


    // Item master data in SDK responses
    [Serializable]
    public class SPItemResponseData : SPItemResponseBaseData, ISpecterMasterData
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

        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<SPPriceResponseData> prices { get; set; }
    }

    public class SPItemResponseExtendedData : SPItemResponseData
    {
        public bool? isDefaultLoadout { get; set; }

    }

    [Serializable]
    public class SPItemResponseDataList : SPResponseDataList<SPItemResponseExtendedData> { }

    #endregion

    //Bundle data in SDK response

    #region Bundle Response Data Models
    [Serializable]
    public class SPBundleResponseData : SPItemResponseData
    {
        public bool? isManual { get; set; }
        public SPBundleContent Contents { get; set; }

    }


    public class SPBundleContent
    {
        public List<SPItemResponseBaseData> items { get; set; }
        public List<SPItemResponseBaseData> bundles { get; set; }
        public List<SPCurrencyInfoResponseData> currencies { get; set; }
    }


    [Serializable]
    public class SPBundleResponseDataList : SPResponseDataList<SPBundleResponseData> { }

    #endregion

    //Store item data in SDK responses

    #region Store Response Data Models
    [Serializable]


    public class SPStoreItemResponseData : SPItemResponseBaseData
    {
        public List<SPPriceResponseData> prices { get; set; }

    }

    public class SPStoreBundleResponseData : SPItemResponseBaseData
    {
        public List<SPPriceResponseData> prices { get; set; }
    }

    public class SPStoreCurrencyResponseData : SPCurrencyInfoResponseData
    {
        public List<SPPriceResponseData> prices { get; set; }
    }


    #endregion

    // User inventory item data in SDK responses

    #region Inventory Response Data Models
    [Serializable]
    public class SPInventoryItemResponseData : SPItemResponseBaseData
    {
        public string collectionId { get; set; }
        public int totalUsesAvailable { get; set; }
        public bool isEquipped { get; set; }
    }

    [Serializable]
    public class SPInventoryBundleResponseData : SPItemResponseBaseData
    {
        public string collectionId { get; set; }
        public int totalUsesAvailable { get; set; }
        public bool isEquipped { get; set; }
        public bool isManual { get; set; }

    }

    public class SPGetUserInventoryResponseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items;
        public List<SPInventoryBundleResponseData> bundles;
    }
    #endregion


    #region Price Response Data Models
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
        public SPCurrencyResponseExtendedData virtualCurrency { get; set; }
        // This is an actual currency (eg: USD, INR, etc.)
        public SPRealWorldCurrencyResponseData realWorldCurrency { get; set; }
    }
    #endregion
}