using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Inventory
{
    /// <summary>
    /// Represents an item to be consumed from the inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPConsumeItemInfo
    {
        /// <summary>
        /// Instance ID of the item to be consumed.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Amount of the item to consume.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Collection ID if applicable.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Additional customization parameters for the item.
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the item.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Represents a request to consume items from the user's inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPConsumeItemRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be consumed from the inventory.
        /// </summary>
        public List<SPConsumeItemInfo> items { get; set; }
    }
}