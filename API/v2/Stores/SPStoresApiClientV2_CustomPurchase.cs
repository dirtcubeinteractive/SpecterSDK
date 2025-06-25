using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Stores
{
    /// <summary>
    /// Represents a request to process a custom purchase with specified items and bundles, allowing overrides to configured pricing.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCustomPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items with custom pricing to be purchased.
        /// </summary>
        public List<SPCustomPurchaseItemInfoV2> items { get; set; }
        
        /// <summary>
        /// Array of bundles with custom pricing to be purchased.
        /// </summary>
        public List<SPCustomPurchaseBundleInfoV2> bundles { get; set; }
    }

    public class SPCustomPurchaseResult : SpecterApiResultBase<SPCustomPurchaseResponse>
    {
        public List<SPInventoryItem> Items { get; set; }
        public List<SPInventoryBundle> Bundles { get; set; }
        
        public List<SPFailedInventoryEntityInfo> ItemsFailed { get; set; }
        public List<SPFailedInventoryEntityInfo> BundlesFailed { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = Response.data?.items?.ConvertAll(x => new SPInventoryItem(x)) ?? new List<SPInventoryItem>();
            Bundles = Response.data?.bundles?.ConvertAll(x => new SPInventoryBundle(x)) ?? new List<SPInventoryBundle>();
            
            ItemsFailed = Response.data?.itemsFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Item)) ?? new List<SPFailedInventoryEntityInfo>();
            BundlesFailed = Response.data?.bundlesFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Bundle)) ?? new List<SPFailedInventoryEntityInfo>();
        }
    }

    public partial class SPStoresApiClientV2
    {
        public async Task<SPCustomPurchaseResult> CustomPurchaseAsync(SPCustomPurchaseRequest request)
        {
            var result = await PostAsync<SPCustomPurchaseResult, SPCustomPurchaseResponse>("/v2/client/stores/custom-purchase", AuthType, request);
            return result;
        }
    }
}