using System;
using System.Collections.Generic;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPRewardHistoryEntryDataV2
    {
        public string instanceId { get; set; }
        public SPRewardClaimStatus status { get; set; }
        public SPRewardSourceType sourceType { get; set; }
        public string sourceId { get; set; }
        public SPRewardsData rewardDetails { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPRewardedItemData : ISpecterRewardedResourceData, ISpecterEconomyResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
        public long amount { get; set; }
    }

    [Serializable]
    public class SPRewardedBundleData : ISpecterRewardedResourceData, ISpecterEconomyResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
        public long amount { get; set; }
    }

    [Serializable]
    public class SPRewardedCurrencyData : ISpecterRewardedResourceData, ISpecterCurrencyData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRarityData rarity { get; set; }
        public long amount { get; set; }

        public string code { get; set; }
        public SPCurrencyType type { get; set; }
    }

    [Serializable]
    public class SPRewardedProgressionMarkerData : ISpecterRewardedResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public long amount { get; set; }
    }

    [Serializable]
    public class SPRewardsData
    {
        public List<SPRewardedItemData> items { get; set; }
        public List<SPRewardedBundleData> bundles { get; set; }
        public List<SPRewardedProgressionMarkerData> progressionMarkers { get; set; }
        public List<SPRewardedCurrencyData> currencies { get; set; }
    }

    [Serializable]
    public class SPPrizeDistributionData
    {
        public List<SPPrizeDistributionRuleData> rules { get; set; }
        public string timeOffsetSeconds { get; set; }
    }

    [Serializable]
    public class SPPrizeDistributionRuleData
    {
        /// <summary>
        /// Sort order of the rule.
        /// </summary>
        public int no { get; set; }

        /// <summary>
        /// Start rank of the rule.
        /// </summary>
        public int startRank { get; set; }

        /// <summary>
        /// End rank of the rule. Null end rank means this rule applies till the last participant.
        /// </summary>
        public int? endRank { get; set; }

        /// <summary>
        /// Rewards of the rule.
        /// </summary>
        public SPRewardsData rewardDetails { get; set; }
    }
    
    [Serializable]
    public class SPFailedRewardsData
    {
        public SPFailedRewardSourceData source { get; set; }
        
        public List<SPFailedInventoryEntityData> itemsFailed { get; set; }
        public List<SPFailedInventoryEntityData> bundlesFailed { get; set; }
        public List<SPFailedResourceInfoData> currenciesFailed { get; set; }
        public List<SPFailedResourceInfoData> progressionMarkersFailed { get; set; }
    }

    [Serializable]
    public class SPFailedRewardSourceData : ISpecterRewardSourceData
    {
        public string id { get; set; }
        public SPRewardSourceType type { get; set; }
        public string instanceId { get; set; }
    }

    /// <summary>
    /// Base data model for resources that failed to be granted to a player (e.g. currency, item, bundle).
    /// </summary>
    [Serializable]
    public class SPFailedResourceInfoData
    {
        public string id { get; set; }
        public string reason { get; set; }
        public string message { get; set; }
        public long amount { get; set; }
        public int code { get; set; }
    }

    /// <summary>
    /// Data model for resources that failed an inventory action (add/remove).
    /// </summary>
    [Serializable]
    public class SPFailedInventoryEntityData : SPFailedResourceInfoData
    {
        public string stackId { get; set; }
        public string collectionId { get; set; }
    }
}