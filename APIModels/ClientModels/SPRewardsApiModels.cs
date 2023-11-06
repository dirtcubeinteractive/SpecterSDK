using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    public sealed class SPRewardClaimStatus : SPEnum<SPRewardClaimStatus>
    {
        public static readonly SPRewardClaimStatus Pending = new SPRewardClaimStatus(0, nameof(Pending).ToLower(), nameof(Pending));
        public static readonly SPRewardClaimStatus Completed = new SPRewardClaimStatus(1, nameof(Completed).ToLower(), nameof(Completed));
        
        private SPRewardClaimStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public class SPRewardBaseData : ISpecterApiResponseData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public int amount { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    [Serializable]
    public class SPCurrencyRewardData : SPRewardBaseData
    {
        public string code { get; set; }
        public SPCurrencyType type { get; set; }
    }
    
    [Serializable]
    public class SPProgressionMarkerRewardData : SPRewardBaseData { }

    [Serializable]
    public abstract class SPItemRewardBaseData : SPRewardBaseData { }
    
    [Serializable]
    public class SPItemRewardData : SPItemRewardBaseData { }
    
    [Serializable]
    public class SPBundleRewardData : SPItemRewardBaseData { }

    // Reward data in SDK responses
    [Serializable]
    public class SPRewardDetailsResponseData : ISpecterApiResponseData
    {
        public List<SPItemRewardData> items { get; set; }
        public List<SPBundleRewardData> bundles { get; set; }
        public List<SPCurrencyRewardData> currencies { get; set; }
        public List<SPProgressionMarkerRewardData> progressionMarkers { get; set; }
    }

    [Serializable]
    public class SPGrantRewardsResponseData : ISpecterApiResponseData
    {
        public List<SPInventoryItemResponseData> items { get; set; }
        public List<SPInventoryBundleResponseData> bundles { get; set; }
        public List<SPWalletCurrencyResponseData> currencies { get; set; }
        public List<SPUpdatedProgressResponseData> progress { get; set; }
    }

    [Serializable]
    public class SPRewardHistoryEntryData : SPRewardBaseData
    {
        public SPRewardClaimStatus status { get; set; }
        public SPRewardClaimType rewardGrant { get; set; }
    }
    
    public class SPGetRewardHistoryResponseData : ISpecterApiResponseData
    {
        public List<SPRewardHistoryEntryData> items { get; set; }
        public List<SPRewardHistoryEntryData> bundles { get; set; }
        public List<SPRewardHistoryEntryData> currencies { get; set; }
        public List<SPRewardHistoryEntryData> progressionMarkers { get; set; }
    }
}