using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;

namespace SpecterSDK.API.ClientAPI.v2.App.DTOs
{
    public interface ISpecterGameData : ISpecterResourceData
    {
        public string howTo { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> videoUrls { get; set; }
        public List<SPAppPlatformData> platforms { get; set; }
        public List<SPLocationData> locations { get; set; }
        public List<SPGameGenreData> genres { get; set; }
    }

    public interface ISpecterPricingCurrencyData
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public interface ISpecterVirtualGoodsProps
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

    public interface ISpecterPurchasableData
    {
        public List<SPPriceDataV2> prices { get; set; }
    }
}