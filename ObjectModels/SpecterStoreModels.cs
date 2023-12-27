using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterStore : SpecterResource, ISpecterMasterObject
    {
        public int CategoriesCount;
        public List<SpecterLocation> StoreLocations;
        public List<SpecterGamePlatformInfoBase> StorePlatforms;
        public List<SpecterUnlockCondition> UnlockConditions;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterStore(SPStoreResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            CategoriesCount = data.categoriesCount;
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, string>();
            
            UnlockConditions = new List<SpecterUnlockCondition>();
            foreach (var unlockCondition in data.unlockConditions)
                UnlockConditions.Add(new SpecterUnlockCondition(unlockCondition));

            StorePlatforms = new List<SpecterGamePlatformInfoBase>();
            foreach (var platformData in data.storePlatforms)
            {
                StorePlatforms.Add(new SpecterGamePlatformInfoBase(platformData));
            }

            StoreLocations = new List<SpecterLocation>();
            foreach (var locationData in data.storeLocations)
            {
                StoreLocations.Add(new SpecterLocation(locationData));
            }
        }
    }

    public class SpecterStoreCategory : SpecterResource
    {
        public int ContentsCount;
        
        public SpecterStoreCategory(SPStoreCategoryResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            ContentsCount = data.contentsCount;
        }
    }


}