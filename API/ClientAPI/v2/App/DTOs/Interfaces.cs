using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.v2;
using SPPrizeDistributionData = SpecterSDK.APIModels.ClientModels.v2.SPPrizeDistributionData;

namespace SpecterSDK.API.ClientAPI.v2.App.DTOs
{
    public interface ISpecterMasterResponse : ISpecterApiResponseData
    {
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }
    
    public interface ISpecterGameData : ISpecterResourceData
    {
        public string howTo { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> videoUrls { get; set; }
        public List<SPAppPlatformData> platforms { get; set; }
        public List<SPLocationData> locations { get; set; }
        public List<SPGameGenreData> genres { get; set; }
    }

    public interface ISpecterCurrencyData : ISpecterResourceData
    {
        public string code { get; set; }
        public SPCurrencyTypeV2 type { get; set; }
    }

    public interface ISpecterPricingCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public interface ISpecterPriceData
    {
        public SPPriceTypes priceType { get; set; }
        
        public long price { get; set; }
        public float? discount { get; set; }
        public float? bonusCashAllowance { get; set; }
        
        public SPPricingCurrencyData currencyDetails { get; set; }
        public SPRealWorldCurrencyData realWorldCurrency { get; set; }
    }

    public interface ISpecterVirtualGoodsPropsData
    {
        public bool isConsumable { get; set; }
        public bool isTradable { get; set; }
        public bool isStackable { get; set; }
        public bool isLocked { get; set; }
        public int stackCapacity { get; set; }
        public int maxCollectionInstance { get; set; }
        public int quantity { get; set; }
        public int limitedQuantity { get; set; }
        public int? consumeByUses { get; set; }
        public int? consumeByTime { get; set; }
        public string consumeByTimeFormat { get; set; }
    }

    public interface ISpecterItemPropData
    {
        public bool isEquippable { get; set; }
        public bool isDefaultLoadout { get; set; }
        public bool equippedByDefault { get; set; }
    }

    public interface ISpecterUnlockableData
    {
        public SPUnlockConditionsData unlockConditions { get; set; }
    }
    
    public interface ISpecterRewardedResourceData : ISpecterResourceData
    {
        public long amount { get; set; }
    }

    public interface ISpecterPurchasableData
    {
        public List<SPPriceDataV2> prices { get; set; }
    }

    public interface ISpecterLeaderboardData : ISpecterLiveOpsEntityData
    {
        public SPLeaderboardSourceData source { get; set; }
        public SPMatchResourceData match { get; set; }
        public SPPrizeDistributionData prizeDistribution { get; set; }
    }

    public interface ISpecterCompetitionData : ISpecterLeaderboardData
    {
        public SPCompetitionConfigData config { get; set; }
        public SPCompetitionFormatData type { get; set; }
        public List<SPEntryFeeDataV2> entryFees { get; set; }
    }

    public interface ISpecterTaskGroupData
    {
        public SPTaskGroupType taskGroupType { get; set; }
        public List<SPTaskResourceData> tasks { get; set; }
    }

    public interface ISpecterLiveOpsEntityData
    {
        public SPScheduleData schedule { get; set; }
    }
}