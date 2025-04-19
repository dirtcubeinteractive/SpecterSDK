using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Others
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
        /// The field to sort inventory items by.
        /// </summary>
        public string sortField { get; set; }
        
        /// <summary>
        /// The sort order for inventory items.
        /// </summary>
        public SPSortOrder sortOrder { get; set; }
        
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
}