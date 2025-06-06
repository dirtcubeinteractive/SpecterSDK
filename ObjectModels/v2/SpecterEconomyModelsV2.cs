using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPBundleProps : ISpecterVirtualGoodsProps
    {
        public bool IsConsumable { get; set; }
        public bool IsTradable { get; set; }
        public bool IsStackable { get; set; }
        public bool IsLocked { get; set; }
        public int StackCapacity { get; set; }
        public int MaxCollectionInstances { get; set; }
        public int Quantity { get; set; }
        public int LimitedQuantity { get; set; }
        public int? ConsumeByUses { get; set; }
        public int? ConsumeByTime { get; set; }
        public string ConsumeByTimeFormat { get; set; }

        public SPBundleProps() {}
        public SPBundleProps(SPBundlePropData data)
        {
            IsConsumable = data.isConsumable;
            IsTradable = data.isTradable;
            IsStackable = data.isStackable;
            IsLocked = data.isLocked;
            StackCapacity = data.stackCapacity;
            MaxCollectionInstances = data.maxCollectionInstance;
            Quantity = data.quantity;
            LimitedQuantity = data.limitedQuantity;
            ConsumeByUses = data.consumeByUses;
            ConsumeByTime = data.consumeByTime;
            ConsumeByTimeFormat = data.consumeByTimeFormat;
        }
    }

    public class SPItemProps : ISpecterItemProps
    {
        public bool IsConsumable { get; set; }
        public bool IsTradable { get; set; }
        public bool IsStackable { get; set; }
        public bool IsLocked { get; set; }
        public int StackCapacity { get; set; }
        public int MaxCollectionInstances { get; set; }
        public int Quantity { get; set; }
        public int LimitedQuantity { get; set; }
        public int? ConsumeByUses { get; set; }
        public int? ConsumeByTime { get; set; }
        public string ConsumeByTimeFormat { get; set; }
        
        public bool IsEquippable { get; set; }
        public bool IsDefaultLoadout { get; set; }
        public bool EquippedByDefault { get; set; }
        
        public SPItemProps() { }
        public SPItemProps(SPItemPropData data)
        {
            IsConsumable = data.isConsumable;
            IsTradable = data.isTradable;
            IsStackable = data.isStackable;
            IsLocked = data.isLocked;
            StackCapacity = data.stackCapacity;
            MaxCollectionInstances = data.maxCollectionInstance;
            Quantity = data.quantity;
            LimitedQuantity = data.limitedQuantity;
            ConsumeByUses = data.consumeByUses;
            ConsumeByTime = data.consumeByTime;
            ConsumeByTimeFormat = data.consumeByTimeFormat;
            
            IsEquippable = data.isEquippable;
            IsDefaultLoadout = data.isDefaultLoadout;
            EquippedByDefault = data.equippedByDefault;
        }
    }
    
    public class SPItem : ISpecterResource, ISpecterVirtualGood, ISpecterMasterObject, ISpecterUnlockable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public ISpecterVirtualGoodsProps Properties => ItemProperties;
        public SPItemProps ItemProperties { get; set; }
        public SPUnlockConditions UnlockConditions { get; set; }
        
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SPItem() { }
        public SPItem(SPItemData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            ItemProperties = data.properties == null ? null : new SPItemProps(data.properties);
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPBundle : ISpecterResource, ISpecterVirtualGood, ISpecterMasterObject, ISpecterUnlockable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public ISpecterVirtualGoodsProps Properties => BundleProperties;
        public SPBundleProps BundleProperties { get; set; }
        public SPBundleContents Contents { get; set; }
        public SPUnlockConditions UnlockConditions { get; set; }
        
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SPBundle() { }
        public SPBundle(SPBundleData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            BundleProperties = data.properties == null ? null : new SPBundleProps(data.properties);
            Contents = data.contents == null ? null : new SPBundleContents(data.contents);
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);

            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPBundleContents
    {
        public List<SPBundleResource> Items { get; set; }
        public List<SPBundleResource> Bundles { get; set; }
        public List<SPBundleResource> Currencies { get; set; }

        public SPBundleContents()
        {
            Items = new List<SPBundleResource>();
            Bundles = new List<SPBundleResource>();
            Currencies = new List<SPBundleResource>();
        }
        
        public SPBundleContents(SPBundleContentsData data)
        {
            Items = data.items == null ? new List<SPBundleResource>() : data.items.ConvertAll(x => new SPBundleResource(x));
            Bundles = data.bundles == null ? new List<SPBundleResource>() : data.bundles.ConvertAll(x => new SPBundleResource(x));
            Currencies = data.currencies == null ? new List<SPBundleResource>() : data.currencies.ConvertAll(x => new SPBundleResource(x));
        }
    }

    public class SPBundleResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public int Quantity { get; set; }
        
        public SPBundleResource() { }
        public SPBundleResource(SPBundleResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Quantity = data.quantity;
        }
    }

    public class SPCurrency : ISpecterCurrency, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public string Code { get; set; }
        public SPCurrencyTypeV2 Type { get; set; }
        
        public bool IsVirtual => Type == SPCurrencyTypeV2.Virtual;
        public bool IsReal => Type == SPCurrencyTypeV2.Real;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPCurrency() { }
        public SPCurrency(SPCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Code = data.code;
            Type = data.type;
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }
}