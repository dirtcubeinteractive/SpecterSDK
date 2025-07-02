using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Inventory
{
    /// <summary>
    /// Represents a request to open a bundle in the user's inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPOpenBundleRequest : SPApiRequestBase
    {
        /// <summary>
        /// Instance ID of the bundle to be opened.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique identifier for the bundle.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Collection ID if applicable.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Stack ID associated with the bundle.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the bundle.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPOpenBundleResult : SpecterApiResultBase<SPOpenBundleResponse>
    {
        public List<SPInventoryItem> Items { get; set; }
        public List<SPInventoryBundle> Bundles { get; set; }
        public List<SPWalletCurrency> Currencies { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = Response.data?.items?.ConvertAll(x => new SPInventoryItem(x)) ?? new List<SPInventoryItem>();
            Bundles = Response.data?.bundles?.ConvertAll(x => new SPInventoryBundle(x)) ?? new List<SPInventoryBundle>();
            Currencies = Response.data?.currencies?.ConvertAll(x => new SPWalletCurrency(x)) ?? new List<SPWalletCurrency>();
        }
    }

    public partial class SPInventoryApiClientV2
    {
        public async Task<SPOpenBundleResult> OpenBundleAsync(SPOpenBundleRequest request)
        {
            var result = await PostAsync<SPOpenBundleResult, SPOpenBundleResponse>("/v2/client/inventory/open-bundle", AuthType, request);
            return result;
        }
    }
}