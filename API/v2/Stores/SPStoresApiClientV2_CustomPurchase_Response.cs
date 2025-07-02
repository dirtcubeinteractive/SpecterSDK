using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Stores
{
    [Serializable]
    public class SPCustomPurchaseResponse : ISpecterApiResponseData
    {
        public List<SPInventoryItemData> items { get; set; }
        public List<SPInventoryBundleData> bundles { get; set; }
        
        public List<SPFailedInventoryEntityData> itemsFailed { get; set; }
        public List<SPFailedInventoryEntityData> bundlesFailed { get; set; }
    }
}