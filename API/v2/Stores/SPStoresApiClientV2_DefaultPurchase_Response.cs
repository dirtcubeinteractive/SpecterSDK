using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Stores
{
    public class SPDefaultPurchaseResponse : ISpecterApiResponseData
    {
        public List<SPInventoryItemData> items { get; set; }
        public List<SPInventoryBundleData> bundles { get; set; }
        
        public List<SPFailedInventoryEntityData> itemsFailed { get; set; }
        public List<SPFailedInventoryEntityData> bundlesFailed { get; set; }
    }
}