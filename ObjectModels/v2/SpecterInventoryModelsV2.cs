using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPInventoryItem : ISpecterInventoryEntity
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        public SPResourceType ResourceType => SPResourceType.Item;
        public long Amount => Quantity;
        
        public string InstanceId { get; set; }
        public string CollectionId { get; set; }
        public string StackId { get; set; }
        public long Quantity { get; set; }
        public bool IsEquipped { get; set; }
        public int? TotalUsesAvailable { get; set; }
        
        public SPInventoryItem() { }
        public SPInventoryItem(SPInventoryItemData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Rarity = (SPRarity)data.rarity.id;
            
            InstanceId = data.instanceId;
            CollectionId = data.collectionId;
            StackId = data.stackId;
            Quantity = data.quantity;
            IsEquipped = data.isEquipped;
            TotalUsesAvailable = data.totalUsesAvailable;
        }
    }
    
    public class SPInventoryBundle : ISpecterInventoryEntity
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        public SPResourceType ResourceType => SPResourceType.Bundle;
        public long Amount => Quantity;
        
        public string InstanceId { get; set; }
        public string CollectionId { get; set; }
        public string StackId { get; set; }
        public long Quantity { get; set; }
        public bool IsEquipped { get; set; }
        public int? TotalUsesAvailable { get; set; }
        
        public SPInventoryBundle() { }
        public SPInventoryBundle(SPInventoryBundleData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Rarity = (SPRarity)data.rarity.id;
            
            InstanceId = data.instanceId;
            CollectionId = data.collectionId;
            StackId = data.stackId;
            Quantity = data.quantity;
            IsEquipped = data.isEquipped;
            TotalUsesAvailable = data.totalUsesAvailable;
        }
    }
}