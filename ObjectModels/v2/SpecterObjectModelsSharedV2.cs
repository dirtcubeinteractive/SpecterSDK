using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    /// <summary>
    /// Information related to a game/app's available platforms.
    /// Used in v2 endpoints.
    /// </summary>
    public class SPAppPlatformInfo
    {
        public SPAppPlatform Platform { get; set; }
        
        /// <summary>
        /// Url to download the game/app's asset bundle.
        /// </summary>
        public string AssetBundleUrl { get; set; }
        
        /// <summary>
        /// Version of the game/app's asset bundle.
        /// </summary>
        public string AssetBundleVersion { get; set; }
        
        /// <summary>
        /// A minimum version of the game/app required for the latest asset bundle to work with.
        /// </summary>
        public string MinimumAppVersion { get; set; }

        public SPAppPlatformInfo(SPAppPlatformData data)
        {
            Platform = (SPAppPlatform)data.id;
            AssetBundleUrl = data.assetBundleUrl;
            AssetBundleVersion = data.assetBundleVersion;
            MinimumAppVersion = data.minimumGameVersion;
        }
    }
    
    public class SPUnlockConditions
    {
        public bool IsLockedByLevel => Levels is { Count: > 0 };
        public bool IsLockedByItem => Items is { Count: > 0 };
        public bool IsLockedByBundle => Bundles is { Count: > 0 };
        public bool IsLocked => IsLockedByLevel || IsLockedByItem || IsLockedByBundle;
        
        public List<SPUnlockResource> Items { get; set; }
        public List<SPUnlockResource> Bundles { get; set; }
        public List<SPUnlockLevel> Levels { get; set; }

        public SPUnlockConditions()
        {
            Items = new List<SPUnlockResource>();
            Bundles = new List<SPUnlockResource>();
            Levels = new List<SPUnlockLevel>();
        }

        public SPUnlockConditions(SPUnlockConditionsData data)
        {
            Items = data.unlockItem == null ? new List<SPUnlockResource>() : data.unlockItem.ConvertAll(x => new SPUnlockResource(x));
            Bundles = data.unlockBundle == null ? new List<SPUnlockResource>() : data.unlockBundle.ConvertAll(x => new SPUnlockResource(x));
            Levels = data.unlockLevel == null ? new List<SPUnlockLevel>() : data.unlockLevel.ConvertAll(x => new SPUnlockLevel(x));
        }
    }
        
    public class SPUnlockLevel
    {
        public int LockedLevelNo  { get; set; }
        public SPUnlockResource UnlockProgressionSystem { get; set; }
        
        public SPUnlockLevel() { }
        public SPUnlockLevel(SPUnlockLevelData data)
        {
            LockedLevelNo = data.lockedLevelNo;
            UnlockProgressionSystem = new SPUnlockResource(data.unlockProgressionSystem);
        }
    }

    public class SPUnlockResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public SPUnlockResource() { }
        public SPUnlockResource(SPUnlockResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
        }
    }

    public class SPPricingCurrencyInfo : ISpecterCurrency, ISpecterPricingCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Code { get; set; }
        
        public SPCurrencyType Type { get; set; }
        public bool IsVirtual => Type == SPCurrencyType.Virtual;
        public bool IsReal => Type == SPCurrencyType.Real;
        
        public SPRarity Rarity { get; set; }
        
        public SPPricingCurrencyInfo() { }
        public SPPricingCurrencyInfo(SPPricingCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Rarity = (SPRarity)data.rarity.id;
            Code = data.code;
            Type = (SPCurrencyType)data.type;
        }
    }

    public class SPRealWorldCurrencyInfo : ISpecterPricingCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public string Symbol { get; set; }
        public string CountryName { get; set; }
        
        public SPRealWorldCurrencyInfo() { }
        public SPRealWorldCurrencyInfo(SPRealWorldCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Code = data.code;
            Symbol = data.symbol;
            CountryName = data.countryName;
        }
    }

    public class SPPriceInfo : ISpecterPrice
    {
        public string ProductId { get; set; }

        public SPPriceTypes PriceType { get; set; }
        public float Discount { get; set; }
        public float BonusCashAllowance { get; set; }
        public SPPricingCurrencyInfo CurrencyDetails { get; set; }
        public SPRealWorldCurrencyInfo RealWorldCurrency { get; set; }
        
        public ISpecterPricingCurrency Currency => PriceType == SPPriceTypes.IAP ? RealWorldCurrency : CurrencyDetails;
        
        public SPPriceInfo() { }
        public SPPriceInfo(SPPriceDataV2 data)
        {
            ProductId = data.productId;
            
            PriceType = data.priceType;
            Discount = data.discount ?? 0f;
            BonusCashAllowance = data.bonusCashAllowance ?? 0f;
            CurrencyDetails = data.currencyDetails == null ? null : new SPPricingCurrencyInfo(data.currencyDetails);
            RealWorldCurrency = data.realWorldCurrency == null ? null : new SPRealWorldCurrencyInfo(data.realWorldCurrency);
        }
    }

    public class SPEntryFeeInfo : ISpecterPrice
    {
        public SPPriceTypes PriceType { get; set; }
        public float Discount { get; set; }
        public float BonusCashAllowance { get; set; }
        public SPPricingCurrencyInfo CurrencyDetails { get; set; }
        public SPRealWorldCurrencyInfo RealWorldCurrency { get; set; }
        
        public ISpecterPricingCurrency Currency => PriceType == SPPriceTypes.IAP ? RealWorldCurrency : CurrencyDetails;
        
        public double HostingFee { get; set; }
        public SPHostingFeeTypes HostingFeeType { get; set; }
        
        public SPEntryFeeInfo() { }
        public SPEntryFeeInfo(SPEntryFeeDataV2 data)
        {
            PriceType = data.priceType;
            Discount = data.discount ?? 0f;
            BonusCashAllowance = data.bonusCashAllowance ?? 0f;
            CurrencyDetails = data.currencyDetails == null ? null : new SPPricingCurrencyInfo(data.currencyDetails);
            RealWorldCurrency = data.realWorldCurrency == null ? null : new SPRealWorldCurrencyInfo(data.realWorldCurrency);
            
            HostingFee = data.hostingFee ?? 0f;
            HostingFeeType = data.hostingFeeType;
        }
    }
}