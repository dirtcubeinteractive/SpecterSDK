using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

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
}