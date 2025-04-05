using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Items endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPItemAttribute : SPEnum<SPItemAttribute>
    {
        public static readonly SPItemAttribute Properties = new SPItemAttribute(0, "properties", "Properties");
        public static readonly SPItemAttribute UnlockConditions = new SPItemAttribute(1, "unlockConditions", "Unlock Conditions");
        public static readonly SPItemAttribute Prices = new SPItemAttribute(2, "prices", "Prices");
        public static readonly SPItemAttribute Meta = new SPItemAttribute(3, "meta", "Meta");
        public static readonly SPItemAttribute Tags = new SPItemAttribute(4, "tags", "Tags");
        
        private SPItemAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch items with optional filtering options.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetItemsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of item IDs to fetch specific items.
        /// </summary>
        public List<string> itemIds { get; set; }
        
        /// <summary>
        /// Whether to filter items by locked (true) or unlocked (false) status.
        /// </summary>
        public bool? isLocked { get; set; }
        
        /// <summary>
        /// Whether to filter items by default loadout status (true or false).
        /// </summary>
        public bool? isDefaultLoadout { get; set; }
        
        /// <summary>
        /// Search keyword to filter items by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// A list of tags to filter the items data.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of items to include in the response. Eg usage: SPItemAttribute.Properties
        /// </summary>
        public List<SPItemAttribute> attributes { get; set; }
    }
}