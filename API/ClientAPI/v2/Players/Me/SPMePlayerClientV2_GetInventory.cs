using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get player inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetInventoryRequest : SPPaginatedApiRequest
    {
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
}