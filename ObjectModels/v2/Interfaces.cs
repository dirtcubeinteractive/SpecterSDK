using System.Collections.Generic;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public interface ISpecterGame
    {
        public string HowTo { get; set; }
        public List<string> ScreenshotUrls { get; set; }
        public List<string> VideoUrls { get; set; }
        public List<SPAppPlatformInfo> Platforms { get; set; }
        public List<SpecterLocation> Locations { get; set; }
        public List<SpecterGameGenre> Genres { get; set; }
    }

    public interface ISpecterEconomyResource : ISpecterResource
    {
        public SPRarity Rarity { get; set; }
    }

    public interface ISpecterCurrency : ISpecterEconomyResource
    {
        public string Code { get; set; }
        public SPCurrencyTypeV2 Type { get; set; }
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

    public interface ISpecterUnlockable
    {
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked { get; }
        public bool IsLockedByLevel { get; }
        public bool IsLockedByItem { get; }
        public bool IsLockedByBundle { get; }
    }

    public interface ISpecterRewardable
    {
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards { get; }
    }

    public interface ISpecterRewardedResource : ISpecterResource
    {
        public SPResourceType ResourceType { get; }
        public long Amount { get; set; }
    }
}