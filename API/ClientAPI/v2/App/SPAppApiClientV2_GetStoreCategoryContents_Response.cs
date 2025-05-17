using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetStoreCategoryContentsResponse : ISpecterApiResponseData
    {
        public List<SPStoreEntityData> items { get; set; }
        public List<SPStoreEntityData> bundles { get; set; }
        
        public int totalItemsCount { get; set; }
        public int totalBundlesCount { get; set; }
    }

    [Serializable]
    public class SPStoreEntityData : ISpecterResourceData, ISpecterPurchasableData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }

        public int? quantity { get; set; }
        public List<SPPriceDataV2> prices { get; set; }
    }
}