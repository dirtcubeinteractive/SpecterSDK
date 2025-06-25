using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Inventory
{
    /// <summary>
    /// Represents an item to be removed from the inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveInventoryItemInfo
    {
        /// <summary>
        /// Instance ID of the item to be removed.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the item to be removed.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Collection ID if applicable.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the item.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Represents a request to remove items or bundles from the user's inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveFromInventoryRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be removed from the inventory.
        /// </summary>
        public List<SPRemoveInventoryItemInfo> items { get; set; }
        
        /// <summary>
        /// Array of bundles to be removed from the inventory.
        /// </summary>
        public List<SPRemoveInventoryItemInfo> bundles { get; set; }
    }

    public class SPRemoveFromInventoryResult : SpecterApiResultBase<SPRemoveFromInventoryResponse>
    {
        public List<SPFailedInventoryEntityInfo> ItemsFailed { get; set; }
        public List<SPFailedInventoryEntityInfo> BundlesFailed { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            ItemsFailed = Response?.data?.itemsFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Item)) ?? new List<SPFailedInventoryEntityInfo>();
            BundlesFailed = Response?.data?.bundlesFailed?.ConvertAll(x => new SPFailedInventoryEntityInfo(x, SPResourceType.Bundle)) ?? new List<SPFailedInventoryEntityInfo>();
        }
    }

    public partial class SPInventoryApiClientV2
    {
        public async Task<SPRemoveFromInventoryResult> RemoveFromInventoryAsync(SPRemoveFromInventoryRequest request)
        {
            var result = await PostAsync<SPRemoveFromInventoryResult, SPRemoveFromInventoryResponse>("/v2/client/inventory/remove", AuthType, request);
            return result;
        }
    }
}