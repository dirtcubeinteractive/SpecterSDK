using System;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;

namespace SpecterSDK.ObjectModels.v1
{
    public class SpecterAppPlatform : SpecterPlatformBase
    {
        public string AssetBundleUrl { get; set; }
        public string AssetBundleVersion { get; set; }

        public SpecterAppPlatform(SPAppPlatformData data) : base(data)
        {
            AssetBundleUrl = data.assetBundleUrl;
            AssetBundleVersion = data.assetBundleVersion;
        }
    }

    public class SpecterPlatformBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public SpecterPlatformBase(SPPlatformBaseData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }

    public enum SpecterResourceLockType
    {
        Progression,
        Item,
        Bundle
    }

    public class SpecterUnlockCondition
    {
        public SpecterResourceLockType LockType;
        public string Id;
        public string Name;
        public int? Value;

        public SpecterUnlockCondition()
        {
        }

        public SpecterUnlockCondition(SPUnlockConditionResponseData data)
        {
            LockType = data.unlockProgressionSystem != null
                ? SpecterResourceLockType.Progression
                : (data.unlockItem != null ? SpecterResourceLockType.Item : SpecterResourceLockType.Bundle);
            (Id, Name, Value) = LockType switch
            {
                SpecterResourceLockType.Progression => (data.unlockProgressionSystem!.id,
                    data.unlockProgressionSystem.name, data.lockedLevelNo),
                SpecterResourceLockType.Item => (data.unlockItem!.id, data.unlockItem.name, 1),
                SpecterResourceLockType.Bundle => (data.unlockBundle.id, data.unlockBundle.name, 1),
                _ => throw new NotImplementedException(
                    $"Unlock condition of type {LockType} not implemented. Please report Specter SDK bug")
            };
        }
    }
}