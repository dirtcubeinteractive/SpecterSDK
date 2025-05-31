using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    public interface ISpecterRewardedResourceData : ISpecterResourceData
    {
        public long amount { get; set; }
    }
    
    [Serializable]
    public class SPRewardedResourceData : ISpecterRewardedResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
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
        public long amount { get; set; }
        
        public string code { get; set; }
        public SPCurrencyTypeV2 type { get; set; }
    }

    [Serializable]
    public class SPRewardsData
    {
        public List<SPRewardedResourceData> items { get; set; }
        public List<SPRewardedResourceData> bundles { get; set; }
        public List<SPRewardedResourceData> progressionMarkers { get; set; }
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
}