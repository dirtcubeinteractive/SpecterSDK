using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPStore : ISpecterResource, ISpecterMasterObject, ISpecterUnlockable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<SPAppPlatform> Platforms { get; set; }
        public List<SPLocation> Locations { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPStore() { }
        public SPStore(SPStoreData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);
            Platforms = data.platforms?.ConvertAll(x => (SPAppPlatform)x.id);
            Locations = data.locations?.ConvertAll(x => new SPLocation(x));
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPStoreCategory : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public bool IsDefault { get; set; }
        public int ContentsCount { get; set; }
        
        public SPStoreCategory() { }
        public SPStoreCategory(SPStoreCategoryData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            IsDefault = data.isDefault;
            ContentsCount = data.contentsCount;
        }
    }

    public class SPStoreEntity : ISpecterEconomyResource, ISpecterPurchasable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public SPRarity Rarity { get; set; }
        
        public int Quantity { get; set; }
        public List<SPPriceInfo> Prices { get; set; }
        
        public SPStoreEntity() { }
        public SPStoreEntity(SPStoreEntityData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Rarity = (SPRarity)data.rarity.id;
            Quantity = data.quantity ?? 1;
        }
    }
}