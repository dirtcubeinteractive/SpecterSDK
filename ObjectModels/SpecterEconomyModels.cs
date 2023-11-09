using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.ObjectModels
{
    #region Currencies

    public abstract class SpecterCurrencyBase : SpecterResource
    {
        public string Code;
        public SPCurrencyType Type;

        protected SpecterCurrencyBase(SPCurrencyResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Code = data.code;
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
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
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

    #region Items

    public abstract class SpecterItemBase : SpecterResource
    {
        public bool IsConsumable;
        public bool IsEquippable;
        public bool IsTradable;
        public bool IsStackable;
        public bool IsRentable;
        public int? MaxNumberOfStack;
    }

    public class SpecterItem : SpecterItemBase, ISpecterMasterObject
    {
        public int Quantity;
        public bool IsLocked;
        public int ConsumeByCount;
        public int ConsumeByTime;
        public List<SpecterPrice> Prices;
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        public SpecterItem() { }
        public SpecterItem(SPItemPriceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = data.tags;
            Meta = data.meta;

            UnlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var conditionData in data.unlockConditions)
                {
                    UnlockConditions.Add(new SpecterUnlockCondition(conditionData));
                }
            }

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


    public class SpecterBundle : SpecterItemBase, ISpecterMasterObject
    {
        public int Quantity;
        public bool IsLocked;
        public int ConsumeByCount;
        public int ConsumeByTime;
        public List<SpecterPrice> Prices;
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        public SpecterBundle() { }

        public SpecterBundle(SPBundleContentResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = data.tags;
            Meta = data.meta;

            UnlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var conditionData in data.unlockConditions)
                {
                    UnlockConditions.Add(new SpecterUnlockCondition(conditionData));
                }
            }

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

    public class SpecterStoreItem : SpecterItem
    {
        public string StoreId;

        public SpecterStoreItem() : base() { }
        public SpecterStoreItem(SPStoreItemResponseData data) : base(data)
        {
            StoreId = data.storeId;
        }
    }

    public class SpecterInventoryItem : SpecterItemBase
    {
        public string CollectionId;
        public int Amount;

        public SpecterInventoryItem(SPInventoryItemResponseData data)
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
            MaxNumberOfStack = data.maxNumberOfStack;

            CollectionId = data.collectionId;
            Amount = data.amount;
        }
    }

    public class SpecterInventoryBundle : SpecterItemBase
    {
        public string CollectionId;
        public int Amount;

        public SpecterInventoryBundle(SPInventoryBundleResponseData data)
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
            MaxNumberOfStack = data.maxNumberOfStack;

            CollectionId = data.collectionId;
            Amount = data.amount;


        }
    }

    [Serializable]
    public class SpecterPrice : SpecterObject
    {
        public SPPriceTypes Type;
        public float Price;
        public float Discount;
        public float BonusCashAllowance;
        public int? GamePlatformMasterId;

        public SpecterCurrency VirtualCurrency;
        public SpecterRealCurrency RealCurrency;

        public SpecterPrice(SPPriceResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Type = data.priceType;
            Discount = data.discount;
            BonusCashAllowance = data.bonusCashAllowance;
            GamePlatformMasterId = data.gamePlatformMasterId;
            VirtualCurrency = data.virtualCurrency != null ? new SpecterCurrency(data.virtualCurrency) : null;
            RealCurrency = data.realWorldCurrency != null ? new SpecterRealCurrency(data.realWorldCurrency) : null;
        }
    }

    #endregion
}