using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Inventory
{
    /// <summary>
    /// Represents an item or bundle to be added to the inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPInventoryEntityInfo
    {
        /// <summary>
        /// Unique identifier for the item or bundle.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the item or bundle to be added.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Collection ID if the item belongs to a specific collection.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// ID of the stack associated with the item or bundle.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the item or bundle.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Represents a request to add items or bundles to the user's inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPAddInInventoryRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be added to the inventory.
        /// </summary>
        public List<SPInventoryEntityInfo> items { get; set; }
        
        /// <summary>
        /// Array of bundles to be added to the inventory.
        /// </summary>
        public List<SPInventoryEntityInfo> bundles { get; set; }
    }

    public class SPAddInInventoryResult : SpecterApiResultBase<SPAddInInventoryResponse>
    {
        public List<SPFailedInventoryEntityInfo> ItemsFailed { get; set; }
        public List<SPFailedInventoryEntityInfo> BundlesFailed { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            ItemsFailed = Response.data?.itemsFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Item)) ?? new List<SPFailedInventoryEntityInfo>();
            BundlesFailed = Response.data?.bundlesFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Bundle)) ?? new List<SPFailedInventoryEntityInfo>();
        }
    }

    public partial class SPInventoryApiClientV2
    {
        public async Task<SPAddInInventoryResult> AddInInventoryAsync(SPAddInInventoryRequest request)
        {
            var result = await PostAsync<SPAddInInventoryResult, SPAddInInventoryResponse>("/v2/client/inventory/add", AuthType, request);
            return result;
        }
    }
}