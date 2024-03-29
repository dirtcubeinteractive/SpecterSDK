using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPRewardResourceData : SPResourceResponseData
    {
        public int amount { get; set; }
    }

    // Reward data in SDK responses
    [Serializable]
    public class SPRewardResourceDetailsResponseData : ISpecterApiResponseData
    {
        public List<SPRewardResourceData> items { get; set; }
        public List<SPRewardResourceData> bundles { get; set; }
        public List<SPRewardResourceData> currencies { get; set; }
        public List<SPRewardResourceData> progressionMarkers { get; set; }
    }

    [Serializable]
    public class SPGrantRewardsResponseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items { get; set; }
        public List<SPInventoryBundleResponseData> bundles { get; set; }
        public List<SPWalletCurrencyResponseData> currencies { get; set; }
        public List<SPUserProgressResponseData> progressionMarkers { get; set; }
    }

    [Serializable]
    public class SPRewardHistoryEntryData : SPRewardResourceData
    {
        public SPRewardClaimStatus status { get; set; }
        public SPRewardGrantType rewardGrant { get; set; }
        public SPRewardSourceType sourceType { get; set; }
        public string sourceId { get; set; }
        public string instanceId { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPGetRewardHistoryResponseData : ISpecterApiResponseData
    {
        public List<SPRewardHistoryEntryData> items { get; set; }
        public List<SPRewardHistoryEntryData> bundles { get; set; }
        public List<SPRewardHistoryEntryData> currencies { get; set; }
        public List<SPRewardHistoryEntryData> progressionMarkers { get; set; }
    }
}