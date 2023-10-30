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
        public string Type;
    }

    public class SpecterRealCurrency : SpecterCurrencyBase
    {
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
        
        public SpecterCurrency() { }
        public SpecterCurrency(SPCurrencyResponseData data)
        {
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterWalletCurrency : SpecterCurrencyBase
    {
        public float Balance;

        public SpecterWalletCurrency() { }
        public SpecterWalletCurrency(SPWalletCurrencyResponseData data)
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
        public bool isConsumable;
        public bool isEquippable;
        public bool isTradable;
        public bool isStackable;
        public bool isRentable;
        public int maxNumberOfStack;
    }

    public class SpecterItem : SpecterItemBase, ISpecterMasterObject
    {
        public int quantity;
        public bool isLocked;
        public int consumeByCount;
        public int consumeByTime;
        public List<SpecterPrice> prices;
        public List<SpecterUnlockCondition> unlockConditions;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterItem() { }
        public SpecterItem(SPItemResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = data.tags;
            Meta = data.meta;

            unlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var conditionData in data.unlockConditions)
                {
                    unlockConditions.Add(new SpecterUnlockCondition(conditionData));
                }
            }

            prices = new List<SpecterPrice>();
            if (data.prices != null)
            {
                foreach (var price in data.prices)
                {
                    prices.Add(new SpecterPrice(price));
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
        public int Amount;
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