using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Inventory
{
    public class SPOpenBundleResponse : ISpecterApiResponseData
    {
        public List<SPInventoryItemData> items { get; set; }
        public List<SPInventoryBundleData> bundles { get; set; }
        public List<SPWalletCurrencyData> currencies { get; set; }
    }
}