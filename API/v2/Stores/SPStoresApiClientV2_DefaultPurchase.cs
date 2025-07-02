using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Stores
{
    /// <summary>
    /// Represents a request to process a default purchase with specified items and bundles from a store.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPDefaultPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be purchased.
        /// </summary>
        public List<SPItemPurchaseBaseInfoV2> items { get; set; }
        
        /// <summary>
        /// Array of bundles to be purchased.
        /// </summary>
        public List<SPBundlePurchaseBaseInfoV2> bundles { get; set; }
    }

    public class SPDefaultPurchaseResult : SpecterApiResultBase<SPDefaultPurchaseResponse>
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
        public async Task<SPDefaultPurchaseResult> DefaultPurchaseAsync(SPDefaultPurchaseRequest request)
        {
            var result = await PostAsync<SPDefaultPurchaseResult, SPDefaultPurchaseResponse>("/v2/client/stores/default-purchase", AuthType, request);
            return result;
        }
    }
}