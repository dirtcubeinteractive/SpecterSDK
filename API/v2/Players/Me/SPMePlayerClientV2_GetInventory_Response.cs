using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyInventoryResponse : ISpecterApiResponseData
    {
        public List<SPInventoryItemData> items { get; set; }
        public List<SPInventoryBundleData> bundles { get; set; }
        public int totalItemsCount { get; set; }
        public int totalBundlesCount { get; set; }
    }
}