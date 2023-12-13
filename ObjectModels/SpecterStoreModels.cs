using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterStore : SpecterResource, ISpecterMasterObject
    {
        public int CategoriesCount;
        public int GamePlatformMasterId;
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
            GamePlatformMasterId = data.gamePlatformMasterId;
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, string>();
            UnlockConditions = new List<SpecterUnlockCondition>();
            foreach (var unlockCondition in data.unlockConditions)
                UnlockConditions.Add(new SpecterUnlockCondition(unlockCondition));
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