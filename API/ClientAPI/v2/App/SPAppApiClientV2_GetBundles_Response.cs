using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetBundlesResponse : ISpecterMasterResponse
    {
        public List<SPBundleData> bundles { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPBundleData : ISpecterResourceData, ISpecterMasterData, ISpecterUnlockableData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPBundlePropData properties { get; set; }
        public SPBundleContentsData contents { get; set; }
        public SPUnlockConditionsData unlockConditions { get; set; }

        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPBundlePropData : ISpecterVirtualGoodsPropsData
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

    [Serializable]
    public class SPBundleContentsData
    {
        public List<SPBundleResourceData> items { get; set; }
        public List<SPBundleResourceData> bundles { get; set; }
        public List<SPBundleResourceData> currencies { get; set; }
    }

    [Serializable]
    public class SPBundleResourceData : ISpecterResourceData
    {
        public string id { get; set; } 
        public string uuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public int quantity { get; set; }
    }
}