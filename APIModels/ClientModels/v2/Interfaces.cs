using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    public interface ISpecterMasterResponse : ISpecterApiResponseData
    {
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    public interface ISpecterBaseUserProfileData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string username { get; set; }
        public string thumbUrl { get; set; }
    }

    public interface ISpecterUserProfileData : ISpecterBaseUserProfileData
    {
        public string email { get; set; }
        public string referralCode { get; set; }
    }
    
    public interface ISpecterGameData : ISpecterResourceData
    {
        public string howTo { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> videoUrls { get; set; }
        public List<SPAppPlatformData> platforms { get; set; }
        public List<SPLocationData> locations { get; set; }
        public List<SPGenreData> genres { get; set; }
    }

    public interface ISpecterCurrencyData : ISpecterEconomyResourceData
    {
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
    }

    public interface ISpecterEconomyResourceData : ISpecterResourceData
    {
        public SPRarityData rarity { get; set; }
    }

    public interface ISpecterTransactionData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public SPTransactionStatus status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public interface ISpecterPlayerOwnedEntityData : ISpecterEconomyResourceData { }

    public interface ISpecterInventoryEntityData : ISpecterPlayerOwnedEntityData
    {
        public string instanceId { get; set; }
        public string collectionId { get; set; }
        public string stackId { get; set; }
        public long quantity { get; set; }
        public bool isEquipped { get; set; }
        public int? totalUsesAvailable { get; set; }
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

    public interface ISpecterRewardSourceData
    {
        public string id { get; set; }
        public SPRewardSourceType type { get; set; }
        public string instanceId { get; set; }
    }
    
    public interface ISpecterRewardedResourceData : ISpecterResourceData
    {
        public long amount { get; set; }
    }

    public interface ISpecterPurchasableData
    {
        public List<SPPriceDataV2> prices { get; set; }
    }

    public interface ISpecterMatchInfoData : ISpecterResourceData
    {
        public SPGameResourceData game { get; set; }
    }

    public interface ISpecterLeaderboardData : ISpecterLiveOpsEntityData
    {
        public SPLeaderboardRankingMethodData rankingMethod { get; set; }
        public SPLeaderboardSourceData source { get; set; }
        public SPMatchResourceData match { get; set; }
        public SPPrizeDistributionData prizeDistribution { get; set; }
    }

    public interface ISpecterLeaderboardInfoData
    {
        public SPLeaderboardSourceData source { get; set; }
        public SPMatchResourceData match { get; set; }
    }

    public interface ISpecterCompetitionData : ISpecterLeaderboardData
    {
        public SPCompetitionConfigData config { get; set; }
        public SPCompetitionFormatData type { get; set; }
        public List<SPEntryFeeDataV2> entryFees { get; set; }
    }

    public interface ISpecterCompetitionInfoData : ISpecterLeaderboardInfoData
    {
        public SPCompetitionConfigData config { get; set; }
        public SPCompetitionFormatData type { get; set; }
        
    }

    public interface ISpecterTaskGroupData : ISpecterTaskGroupResourceData, ISpecterUnlockableData, ISpecterMasterData, ISpecterLiveOpsEntityData
    {
        public List<SPTaskResourceData> tasks { get; set; }
    }

    public interface ISpecterTaskResourceData : ISpecterResourceData
    {
    }

    public interface ISpecterTaskGroupResourceData : ISpecterResourceData
    {
        public SPTaskGroupType taskGroupType { get; set; }
    }

    public interface ISpecterTaskStatusData : ISpecterResourceData
    {
        public string instanceId { get; set; }
        public SPTaskStatus status { get; set; }
    }

    public interface ISpecterRuleParamData
    {
        public string name { get; set; }
        public object targetValue { get; set; }
        public string @operator { get; set; }
        public SPParamDataType dataType { get; set; }
        public SPParamEvalMode mode { get; set; }
        public SPParameterType type { get; set; }
    }

    public interface ISpecterLiveOpsEntityData
    {
        public SPScheduleData schedule { get; set; }
    }
}