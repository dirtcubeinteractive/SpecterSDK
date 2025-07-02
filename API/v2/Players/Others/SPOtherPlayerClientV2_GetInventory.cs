using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get inventory for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerInventoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The ID of the target user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// A search keyword to filter inventory items by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// The ID of the collection to filter inventory items.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// An array of item IDs to fetch specific items.
        /// </summary>
        public List<string> itemIds { get; set; }
        
        /// <summary>
        /// An array of bundle IDs to fetch specific bundles.
        /// </summary>
        public List<string> bundleIds { get; set; }
    }
    
    public class SPGetOtherPlayerInventoryResult : SpecterApiResultBase<SPGetOtherPlayerInventoryResponse>
    {
        public List<SPInventoryItem> Items { get; set; }
        public List<SPInventoryBundle> Bundles { get; set; }
        
        public int TotalItemsCount { get; set; }
        public int TotalBundlesCount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = Response.data?.items?.ConvertAll(x => new SPInventoryItem(x)) ?? new List<SPInventoryItem>();
            Bundles = Response.data?.bundles?.ConvertAll(x => new SPInventoryBundle(x)) ?? new List<SPInventoryBundle>();
            
            TotalItemsCount = Response.data?.totalItemsCount ?? 0;
            TotalBundlesCount = Response.data?.totalBundlesCount ?? 0;
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerInventoryResult> GetOtherPlayerInventoryAsync(SPGetOtherPlayerInventoryRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerInventoryResult, SPGetOtherPlayerInventoryResponse>("/v2/client/player/get-inventory", AuthType, request);
            return result;
        }
    }
}