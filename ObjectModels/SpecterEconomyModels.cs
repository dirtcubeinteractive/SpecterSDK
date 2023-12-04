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
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Type = data.type;
            Code = data.code;
            Balance = data.balance;
        }
    }

    #endregion
    #region Specter Items

    public abstract class SpecterItemMasterBase : SpecterResource, ISpecterMasterObject
    {
        public bool IsConsumable;
        public bool IsEquippable;
        public bool IsTradable;
        public bool IsStackable;
        public int? StackCapacity;
        public bool IsRentable;

        public int? Quantity;
        public bool IsLocked;
        public int? ConsumeByUses;
        public int? ConsumeByTime;

        public List<SpecterPrice> Prices;
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        protected SpecterItemMasterBase() { }
        public SpecterItemMasterBase(SPCollectibleResourceResponseData data)
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
            IsRentable = data.isRentable;
            StackCapacity = data.stackCapacity;

            Quantity = data.quantity;
            IsLocked = data.isLocked;
            ConsumeByUses = data.consumeByUses;
            ConsumeByTime = data.consumeByTime;


            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                {
                    Prices.Add(new SpecterPrice(price));
                }
            }

            UnlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var conditionData in data.unlockConditions)
                {
                    UnlockConditions.Add(new SpecterUnlockCondition(conditionData));
                }
            }

            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterItem : SpecterItemMasterBase
    {
        public bool? IsDefaultLoadout;

        public SpecterItem() { }
        public SpecterItem(SPItemResponseData data) : base(data)
        {
            IsDefaultLoadout = data.isDefaultLoadout;
        }

    }
    #endregion
    #region Specter Bundles




    public class SpecterBundle : SpecterItemMasterBase
    {
        public bool? IsManual;
        public SpecterBundleContent SpecterBundleContent;

        public SpecterBundle(SPBundleResponseData data) : base(data)
        {
            IsManual = data.isManual;
            SpecterBundleContent = new SpecterBundleContent(data.contents);
        }
    }

    public class SpecterBundleContent
    {
        public List<SpecterItemInfo> Items;
        public List<SpecterBundleInfo> Bundles;
        public List<SpecterCurrencyInfo> Currencies;

        public SpecterBundleContent(SPBundleContentsData data)
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

    public class SpecterResourceContentInfo
    {
        public string Uuid;
        public string Id;
        public string Name;
        public string Description;
        public string IconUrl;
        public int Quantity;

    }
    public class SpecterItemInfo : SpecterResourceContentInfo
    {

        public SpecterItemInfo(SPResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    public class SpecterBundleInfo : SpecterResourceContentInfo
    {
        public SpecterBundleInfo(SPResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }

    public class SpecterCurrencyInfo : SpecterResourceContentInfo
    {
        public SpecterCurrencyInfo(SPResourceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }

    #endregion
    #region Specter Store


    public class SpecterStoreItemInfo : SpecterResourceContentInfo
    {
        public List<SpecterPrice> Prices;
        public SpecterStoreItemInfo(SPStoreItemResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                {
                    Prices.Add(new SpecterPrice(price));
                }
            }
        }
    }

    public class SpecterStoreBundleInfo : SpecterResourceContentInfo
    {
        public List<SpecterPrice> Prices;
        public SpecterStoreBundleInfo(SPStoreBundleResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                {
                    Prices.Add(new SpecterPrice(price));
                }
            }
        }
    }

    public class SpecterStoreCurrencyInfo : SpecterResourceContentInfo
    {
        public List<SpecterPrice> Prices;
        public SpecterStoreCurrencyInfo(SPStoreCurrencyResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            Prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                {
                    Prices.Add(new SpecterPrice(price));
                }
            }
        }

    }
    #endregion
    #region Specter Inventory
    public class SpecterInventoryItem : SpecterResource
    {
        public string CollectionId;
        public int TotalUsesAvailable;
        public bool IsEquipped;

        public SpecterInventoryItem(SPInventoryItemResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            CollectionId = data.collectionId;
            TotalUsesAvailable = data.totalUsesAvailable;
            IsEquipped = data.isEquipped;
        }
    }

    public class SpecterInventoryBundle : SpecterResource
    {
        public string CollectionId;
        public int TotalUsesAvailable;
        public bool IsEquipped;
        public bool IsManual;
        public SpecterInventoryBundle(SPInventoryBundleResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            CollectionId = data.collectionId;
            TotalUsesAvailable = data.totalUsesAvailable;
            IsEquipped = data.isEquipped;
            IsManual = data.isManual;
        }
    }
    #endregion
    #region Specter Price
    [Serializable]
    public class SpecterPrice : SpecterObject
    {
        public SPPriceTypes Type;
        public float Price;
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
            Discount = data.discount;
            BonusCashAllowance = data.bonusCashAllowance;
            GamePlatformMasterId = data.gamePlatformMasterId;
            VirtualCurrency = data.virtualCurrency != null ? new SpecterCurrencyBase(data.virtualCurrency) : null;
            RealCurrency = data.realWorldCurrency != null ? new SpecterRealCurrency(data.realWorldCurrency) : null;
        }
    }
    #endregion

}