using System;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPInventoryItemData : ISpecterInventoryEntityData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        /// <summary>
        /// Unique identifier for the inventory entry of this item
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the collection the item belongs to.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the stack the item belongs to.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Quantity within the stack of this item. Can be greater than 1 only if the item is stackable.
        /// </summary>
        public long quantity { get; set; }
        
        /// <summary>
        /// Flag indicating if the item is equipped.
        /// </summary>
        public bool isEquipped { get; set; }
        
        /// <summary>
        /// Number of uses remaining if an item is configured to be consumable by uses.
        /// </summary>
        public int? totalUsesAvailable { get; set; }
        
        public SPRarityData rarity { get; set; }
    }

    [Serializable]
    public class SPInventoryBundleData : ISpecterInventoryEntityData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        /// <summary>
        /// Unique identifier for the inventory instance of this bundle
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the collection the bundle belongs to.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Unique developer defined identifier for the stack the bundle belongs to.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Quantity within the stack of this bundle. Can be greater than 1 only if the bundle is stackable.
        /// </summary>
        public long quantity { get; set; }
        
        /// <summary>
        /// Flag indicating if the bundle is equipped.
        /// </summary>
        public bool isEquipped { get; set; }
        
        /// <summary>
        /// Number of uses remaining if a bundle is configured to be consumable by uses.
        /// </summary>
        public int? totalUsesAvailable { get; set; }
        
        public SPRarityData rarity { get; set; }
    }
}