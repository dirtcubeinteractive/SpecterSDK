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

    public class SpecterStoreCurrency : SpecterCurrency
    {
        public int Quantity;
        public List<SpecterPrice> Prices;

        public SpecterStoreCurrency(SPStoreCurrencyResponseData data) : base(data)
        {
            Quantity = data.quantity;
            Prices = new List<SpecterPrice>();
            foreach (var price in data.prices)
            {
                Prices.Add(new SpecterPrice(price));
            }
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


    public class SpecterItemData : SpecterItemBase
    {
        public int? Quantity;
        public bool IsLocked;
        public int? ConsumeByCount;
        public int? ConsumeByTime;
    }
    public class SpecterItem : SpecterItemData, ISpecterMasterObject
    {

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

            IsConsumable = data.isConsumable;
            IsEquippable = data.isEquippable;
            IsTradable = data.isTradable;
            IsStackable = data.isStackable;
            IsRentable = data.isRentable;
            MaxNumberOfStack = data.maxNumberOfStack;

            Quantity = data.quantity;
            IsLocked = data.isLocked;
            ConsumeByCount = data.consumeByCount;
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

    public class SpecterBundleBase : SpecterItemBase
    {
        public int? Quantity;
        public bool IsLocked;
        public bool? isManual { get; set; }
        public int? ConsumeByCount;
        public int? ConsumeByTime;

    }

    public class SpecterBundleData : SpecterBundleBase, ISpecterMasterObject
    {
        public List<SpecterPrice> Prices;
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SpecterBundleContent SpecterBundleContent;
    }

    public class SpecterBundle : SpecterBundleData
    {
        public SpecterBundle() { }

        public SpecterBundle(SPBundleContentResponseData data)
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

            Quantity = data.quantity;
            IsLocked = data.isLocked;
            isManual = data.isManual;
            ConsumeByCount = data.consumeByCount;
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

            SpecterBundleContent = new SpecterBundleContent(data.Contents);
        }
    }

    public class SpecterStoreBundle : SpecterBundleData
    {
        public SpecterStoreBundle() { }

        public SpecterStoreBundle(SPBundlePriceResponseData data)
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


    public class SpecterBundleContent
    {
        public List<SpecterItemContent> Items;
        public List<SpecterInnerBundleContent> Bundles;
        public List<SpecterCurrencyContent> Currencies;

        public SpecterBundleContent(SPBundleContent data)
        {
            Items = new List<SpecterItemContent>();
            if (data.Items != null)
            {
                foreach (var item in data.Items)
                {
                    Items.Add(new SpecterItemContent(item));
                }
            }


            Bundles = new List<SpecterInnerBundleContent>();
            if (data.Bundles != null)
            {
                foreach (var bundle in data.Bundles)
                {
                    Bundles.Add(new SpecterInnerBundleContent(bundle));
                }
            }

            Currencies = new List<SpecterCurrencyContent>();
            if (data.Currencies != null)
            {
                foreach (var currency in data.Currencies)
                {
                    Currencies.Add(new SpecterCurrencyContent(currency));
                }
            }
        }

    }


    public class SpecterItemContent : SpecterItemData
    {
        public SpecterItemContent(SPItemResponseData data)
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

            Quantity = data.quantity;
            IsLocked = data.isLocked;
            ConsumeByCount = data.consumeByCount;
            ConsumeByTime = data.consumeByTime;
        }
    }

    public class SpecterInnerBundleContent : SpecterBundleBase
    {
        public SpecterInnerBundleContent(SPBundleResponseData data)
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

            Quantity = data.quantity;
            IsLocked = data.isLocked;
            isManual = data.isManual;
            ConsumeByCount = data.consumeByCount;
            ConsumeByTime = data.consumeByTime;
        }
    }

    public class SpecterCurrencyContent : SpecterCurrencyBase
    {
        public SpecterCurrencyContent(SPCurrencyResponseBaseData data) : base(data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Code = data.code;
        }
    }

    #endregion
}