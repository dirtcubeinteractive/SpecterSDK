using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
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
        public List<SPUserProgressResponseData> progress { get; set; }
    }
}