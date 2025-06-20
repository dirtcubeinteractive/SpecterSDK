using System.Collections.Generic;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public interface ISpecterBaseUserProfile
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ThumbUrl { get; set; }
    }
    
    public interface ISpecterGame
    {
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public List<SPAppPlatformInfo> Platforms { get; set; }
        public List<SPLocation> Locations { get; set; }
        public List<SPGenre> Genres { get; set; }
    }

    public interface ISpecterMatchInfo : ISpecterResource
    {
        public SPGameResource Game { get; set; }
    }

    public interface ISpecterEconomyResource : ISpecterResource
    {
        public SPRarity Rarity { get; set; }
    }

    public interface ISpecterCurrency : ISpecterEconomyResource
    {
        public string Code { get; set; }
        public SPCurrencyType Type { get; set; }
        public bool IsVirtual { get; }
        public bool IsReal { get; }
    }

    public interface ISpecterPricingCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public interface ISpecterPrice
    {
        /// <summary>
        /// Pricing type - virtual, rmg or IAP.
        /// </summary>
        public SPPriceTypes PriceType { get; set; }
        public float Discount { get; set; }
        public float BonusCashAllowance { get; set; }
        
        /// <summary>
        /// Virtual or RMG currency details.
        /// </summary>
        public SPPricingCurrencyInfo CurrencyDetails { get; set; }
        
        /// <summary>
        /// Info about a real world fiat currency (only available for price type IAP)
        /// </summary>
        public SPRealWorldCurrencyInfo RealWorldCurrency { get; set; }
        
        /// <summary>
        /// Use the Currency getter to access common currency info.
        /// </summary>
        public ISpecterPricingCurrency Currency { get; }
    }

    public interface ISpecterPurchasable
    {
        public List<SPPriceInfo> Prices { get; set; }
    }

    public interface ISpecterVirtualGoodsProps
    {
        public bool IsConsumable { get; set; }
        public bool IsTradable { get; set; }
        public bool IsStackable { get; set; }
        public bool IsLocked { get; set; }
        public int StackCapacity { get; set; }
        public int MaxCollectionInstances { get; set; }
        public int Quantity { get; set; }
        public int LimitedQuantity { get; set; }
        public int? ConsumeByUses { get; set; }
        public int? ConsumeByTime { get; set; }
        public string ConsumeByTimeFormat { get; set; }
    }

    public interface ISpecterItemProps : ISpecterVirtualGoodsProps
    {
        public bool IsEquippable { get; set; }
        public bool IsDefaultLoadout { get; set; }
        public bool EquippedByDefault { get; set; }
    }

    public interface ISpecterVirtualGood
    {
        public ISpecterVirtualGoodsProps Properties { get; }
    }

    public interface ISpecterPlayerOwnedEntity : ISpecterEconomyResource
    {
        /// <summary>
        /// Type of resource.
        /// </summary>
        public SPResourceType ResourceType { get; }
        
        /// <summary>
        /// A standardized property to access amount/quantity/balance of a player-owned resource
        /// when using the ISpecterPlayerOwnedEntity interface.
        /// </summary>
        public long Amount { get; }
    }

    public interface ISpecterInventoryEntity : ISpecterPlayerOwnedEntity
    {
        /// <summary>
        /// Unique identifier for the inventory instance of this entity.
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the collection the entity belongs to.
        /// </summary>
        public string CollectionId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the stack the entity belongs to.
        /// </summary>
        public string StackId { get; set; }
        
        /// <summary>
        /// Quantity within the stack of this entity. Can be greater than 1 only if the entity is stackable.
        /// </summary>
        public long Quantity { get; set; }
        
        /// <summary>
        /// Flag indicating if the entity is equipped.
        /// </summary>
        public bool IsEquipped { get; set; }
        
        /// <summary>
        /// Number of uses remaining if an entity is configured to be consumable by uses.
        /// </summary>
        public int? TotalUsesAvailable { get; set; }
    }

    public interface ISpecterUnlockable
    {
        /// <summary>
        /// Rules defining the conditions to unlock an entity (items, bundles, tasks, etc.)
        /// </summary>
        public SPUnlockConditions UnlockConditions { get; set; }
        
        /// <summary>
        /// Flag indicating if the entity is locked.
        /// </summary>
        public bool IsLocked { get; }
        
        /// <summary>
        /// Flag indicating if the entity is locked by a progression system level.
        /// </summary>
        public bool IsLockedByLevel { get; }
        
        /// <summary>
        /// Flag indicating if the entity is locked by an item.
        /// </summary>
        public bool IsLockedByItem { get; }
        
        /// <summary>
        /// Flag indicating if the entity is locked by a bundle.
        /// </summary>
        public bool IsLockedByBundle { get; }
    }

    public interface ISpecterRewardable
    {
        /// <summary>
        /// Information about the rewards for the entity.
        /// </summary>
        public SPRewards RewardDetails { get; set; }
        
        /// <summary>
        /// Flag indicating if the entity has rewards.
        /// </summary>
        public bool HasRewards { get; }
        
        public SPRewardSourceType RewardSource { get; }
    }

    public interface ISpecterRewardedResource : ISpecterResource
    {
        /// <summary>
        /// Type of rewarded resource.
        /// </summary>
        public SPResourceType ResourceType { get; }
        
        /// <summary>
        /// Amount of the rewarded resource.
        /// </summary>
        public long Amount { get; set; }
    }

    public interface ISpecterTaskStatusInfo : ISpecterResource
    {
        public string InstanceId { get; set; }
        public SPTaskStatus Status { get; set; }
    }

    public interface ISpecterTaskGroupResource : ISpecterResource
    {
        public SPTaskGroupType TaskGroupType { get; set; }
    }

    public interface ISpecterRuleParam
    {
        public string Name { get; set; }
        public object TargetValue { get; set; }
        public string Operator { get; set; }
        public SPParamDataType DataType { get; set; }
        public SPParamEvalMode Mode { get; set; }
        public SPParameterType Type { get; set; }
    }

    public interface ISpecterLiveOpsEntity
    {
        public SPSchedule Schedule { get; set; }
    }

    public interface ISpecterLeaderboard : ISpecterLiveOpsEntity
    {
        /// <summary>
        /// The ranking method of the leaderboard or competition. Not applicable for Instant Battles.
        /// </summary>
        public SPRankingMethod RankingMethod { get; set; }
        
        /// <summary>
        /// The source of the leaderboard or competition score.
        /// </summary>
        public SPLeaderboardSourceType Source { get; set; }
        
        /// <summary>
        /// Info about the match backing the leaderboard or competition. Only applicable if the source is a match.
        /// </summary>
        public SPMatchResource Match { get; set; }
        
        /// <summary>
        /// Prize distribution for the leaderboard or competition.
        /// </summary>
        public SPPrizeDistribution PrizeDistribution { get; set; }
    }

    public interface ISpecterLeaderboardInfo
    {
        /// <summary>
        /// The source of the leaderboard or competition score.
        /// </summary>
        public SPLeaderboardSourceType Source { get; set; }
        
        /// <summary>
        /// Info about the match backing the leaderboard or competition. Only applicable if the source is a match.
        /// </summary>
        public SPMatchResource Match { get; set; }
    }

    public interface ISpecterCompetition : ISpecterLeaderboard
    {
        /// <summary>
        /// Configuration details for the competition.
        /// </summary>
        SPCompetitionConfig Config { get; set; }
        
        /// <summary>
        /// Type of the competition. (E.g. Tournament, Instant Battle)
        /// </summary>
        SPCompetitionFormat Type { get; set; }
        
        /// <summary>
        /// Entry fee structure for the competition.
        /// </summary>
        public List<SPEntryFeeInfo> EntryFees { get; set; }
    }

    public interface ISpecterCompetitionInfo : ISpecterLeaderboardInfo
    {
        /// <summary>
        /// Configuration details for the competition.
        /// </summary>
        SPCompetitionConfig Config { get; set; }
        
        /// <summary>
        /// Type of the competition. (E.g. Tournament, Instant Battle)
        /// </summary>
        SPCompetitionFormat Type { get; set; }
    }

    public interface ISpecterCompetitionHistoryEntry : ISpecterCompetitionInfo
    {
        /// <summary>
        /// Information about all the entries a player has made in the competition.
        /// </summary>
        public List<SPCompetitionEntryInfo> EntryDetails { get; set; }
    }
}