using System;
using System.Collections.Generic;
using System.Data;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.ObjectModels
{
    #region Specter Currencies
    public class SpecterCurrencyBase : SpecterResource
    {
        public string Code;
        public SPCurrencyType Type;

        public SpecterCurrencyBase(SPCurrencyResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Code = data.code;
            Type = data.type;
        }
    }

    public class SpecterRealCurrency
    {
        public string Uuid;
        public string Id;
        public string Name;
        public string Code;
        public string CountryName;
        public string ASCIISymbol;
        public SpecterRealCurrency(SPRealWorldCurrencyResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            CountryName = data.countryName;
            Code = data.code;
            ASCIISymbol = data.symbol;
        }
    }

    public class SpecterCurrency : SpecterCurrencyBase, ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SpecterCurrency(SPCurrencyResponseData data) : base(data)
        {
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterWalletCurrency : SpecterCurrencyBase
    {
        public float Balance;
        public SpecterWalletCurrency(SPWalletCurrencyResponseData data) : base(data)
        {
            Balance = data.balance;
        }
    }
    #endregion

    #region Specter Items
    public abstract class SpecterCollectibleResourceBase : SpecterResource, ISpecterMasterObject
    {
        public bool IsConsumable;
        public bool IsEquippable;
        public bool IsTradable;
        public bool IsStackable;
        public int? StackCapacity;
        public int? MaxCollectionInstance;
        public bool IsRentable;
        public bool IsLocked;
        public int? ConsumeByUses;
        public int? ConsumeByTime;
        public int? Quantity;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<SpecterPrice> Prices;

        protected SpecterCollectibleResourceBase() { }
        public SpecterCollectibleResourceBase(SPCollectibleResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            IsConsumable = data.isConsumable;
            IsEquippable = data.isEquippable;
            IsTradable = data.isTradable;
            IsStackable = data.isStackable;
            StackCapacity = data.stackCapacity;
            MaxCollectionInstance = data.maxCollectionInstance;
            IsRentable = data.isRentable;
            IsLocked = data.isLocked;
            ConsumeByUses = data.consumeByUses;
            ConsumeByTime = data.consumeByTime;
            Quantity = data.quantity;
            Tags = data.tags;
            Meta = data.meta;
            UnlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var conditionData in data.unlockConditions)
                    UnlockConditions.Add(new SpecterUnlockCondition(conditionData));
            }

            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                    Prices.Add(new SpecterPrice(price));
            }


        }
    }

    public class SpecterItem : SpecterCollectibleResourceBase
    {
        public bool? IsDefaultLoadout;
        public SpecterItem() { }
        public SpecterItem(SPItemResponseData data) : base(data) => IsDefaultLoadout = data.isDefaultLoadout;

    }
    #endregion
    #region Specter Bundles

    public class SpecterBundle : SpecterCollectibleResourceBase
    {
        public bool? IsManual;
        public SpecterBundleContents SpecterBundleContent;
        public SpecterBundle(SPBundleResponseData data) : base(data)
        {
            IsManual = data.isManual;
            SpecterBundleContent = new SpecterBundleContents(data.contents);
        }
    }

    public class SpecterBundleContents
    {
        public List<SpecterItemInfo> Items;
        public List<SpecterBundleInfo> Bundles;
        public List<SpecterCurrencyInfo> Currencies;

        public SpecterBundleContents(SPBundleContentsData data)
        {
            Items = new List<SpecterItemInfo>();
            if (data.items != null)
            {
                foreach (var item in data.items)
                    Items.Add(new SpecterItemInfo(item));
            }
            Bundles = new List<SpecterBundleInfo>();
            if (data.bundles != null)
            {
                foreach (var bundle in data.bundles)
                    Bundles.Add(new SpecterBundleInfo(bundle));
            }
            Currencies = new List<SpecterCurrencyInfo>();
            if (data.currencies != null)
            {
                foreach (var currency in data.currencies)
                    Currencies.Add(new SpecterCurrencyInfo(currency));
            }
        }

    }

    public class SpecterItemInfo : SpecterBundleResource { public SpecterItemInfo(SPBundleResourceData data) : base(data) { } }
    public class SpecterBundleInfo : SpecterBundleResource { public SpecterBundleInfo(SPBundleResourceData data) : base(data) { } }
    public class SpecterCurrencyInfo : SpecterBundleResource { public SpecterCurrencyInfo(SPBundleResourceData data) : base(data) { } }
    public class SpecterBundleResource : SpecterResource
    {
        public int Quantity;
        public SpecterBundleResource(SPBundleResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Quantity = data.quantity;
        }
    }
    #endregion

    #region Specter Store
    public class SpecterStoreResource : SpecterResource
    {
        public int Quantity;
        public List<SpecterPrice> Prices;
        public SpecterStoreResource(SPStoreResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Quantity = data.quantity;
            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                    Prices.Add(new SpecterPrice(price));
            }
        }
    }
    public class SpecterStoreItemInfo : SpecterStoreResource { public SpecterStoreItemInfo(SPStoreItemResponseData data) : base(data) { } }
    public class SpecterStoreBundleInfo : SpecterStoreResource { public SpecterStoreBundleInfo(SPStoreBundleResponseData data) : base(data) { } }
    public class SpecterStoreCurrencyInfo : SpecterStoreResource { public SpecterStoreCurrencyInfo(SPStoreCurrencyResponseData data) : base(data) { } }

    public class SpecterPurchasedResourceInfo : SpecterResource
    {
        public string SlotId;
        public int TotalUsesAvailable;
        public bool IsEquipped;
        public int Quantity;
        public string CollectionId;
        public string StackId;
        public SpecterPurchasedResourceInfo(SPPurchasedResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            SlotId = data.slotId;
            TotalUsesAvailable = data.totalUsesAvailable;
            IsEquipped = data.isEquipped;
            Quantity = data.quantity;
            CollectionId = data.collectionId;
            StackId = data.stackId;
        }
    }
    public class SpecterPurchasedItemInfo : SpecterPurchasedResourceInfo { public SpecterPurchasedItemInfo(SPPurchasedItemResponseData data) : base(data) { } }
    public class SpecterPurchasedBundleInfo : SpecterPurchasedResourceInfo { public SpecterPurchasedBundleInfo(SPPurchasedBundleResponseData data) : base(data) { } }
    #endregion

    #region Specter Inventory
    public class SpecterInventoryResource : SpecterResource
    {
        public string SlotId;
        public int TotalUsesAvailable;
        public bool IsEquipped;
        public int Quantity;
        public string CollectionId;
        public string StackId;
        public SpecterInventoryResource(SPInventoryResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            SlotId = data.slotId;
            TotalUsesAvailable = data.totalUsesAvailable;
            IsEquipped = data.isEquipped;
            Quantity = data.quantity;
            CollectionId = data.collectionId;
            StackId = data.stackId;
        }
    }

    public class SpecterInventoryItem : SpecterInventoryResource { public SpecterInventoryItem(SPInventoryItemResponseData data) : base(data) { } }

    public class SpecterInventoryBundle : SpecterInventoryResource { public SpecterInventoryBundle(SPInventoryBundleResponseData data) : base(data) { } }

    #endregion

    #region Specter Price
    [Serializable]
    public class SpecterPrice : SpecterObject
    {
        public SPPriceTypes Type;
        public double Price;
        public float Discount;
        public float BonusCashAllowance;
        public int? GamePlatformMasterId;
        public SpecterCurrencyBase VirtualCurrency;
        public SpecterRealCurrency RealCurrency;

        public SpecterPrice(SPPriceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Type = data.priceType;
            Price = data.price;
            Discount = data.discount;
            BonusCashAllowance = data.bonusCashAllowance;
            GamePlatformMasterId = data.gamePlatformMasterId;
            VirtualCurrency = data.virtualCurrency != null ? new SpecterCurrencyBase(data.virtualCurrency) : null;
            RealCurrency = data.realWorldCurrency != null ? new SpecterRealCurrency(data.realWorldCurrency) : null;
        }
    }
    #endregion

}