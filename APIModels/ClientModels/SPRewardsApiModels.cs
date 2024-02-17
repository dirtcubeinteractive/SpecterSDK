using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPRewardBaseData : SPResourceResponseData
    {
        public int amount { get; set; }
    }

    // Reward data in SDK responses
    [Serializable]
    public class SPRewardDetailsResponseData : ISpecterApiResponseData
    {
        public List<SPRewardBaseData> items { get; set; }
        public List<SPRewardBaseData> bundles { get; set; }
        public List<SPRewardBaseData> currencies { get; set; }
        public List<SPRewardBaseData> progressionMarkers { get; set; }
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
    public class SPRewardHistoryEntryData : SPRewardBaseData
    {
        public SPRewardClaimStatus status { get; set; }
        public SPRewardGrantType rewardGrant { get; set; }
        public SPRewardSourceType sourceType { get; set; }
        public string sourceId { get; set; }
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