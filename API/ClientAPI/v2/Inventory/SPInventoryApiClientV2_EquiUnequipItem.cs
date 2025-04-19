using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Inventory
{
    /// <summary>
    /// Represents an item to be equipped or unequipped.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEquipUnequipItemInfo
    {
        /// <summary>
        /// Instance ID of the item to be equipped or unequipped.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Collection ID if applicable.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Indicates if the item should be equipped (true) or unequipped (false).
        /// </summary>
        public bool shouldEquip { get; set; }
        
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
    /// Represents a request to equip or unequip items in the user's inventory.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEquipUnequipItemRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be equipped or unequipped.
        /// </summary>
        public List<SPEquipUnequipItemInfo> items { get; set; }
    }
}