using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    using Interfaces;

    /// <summary>
    /// A class that holds credentials needed for API calls requiring authorization.
    /// </summary>
    [Serializable]
    public class SPAuthContext
    {
        // User access token required for most client API calls
        public string AccessToken;
        // Entity token required for certain client API calls such as to refresh an Access Token
        public string EntityToken;
    }
    
    /// <summary>
    /// Base class for all Specter objects. Specter objects are designed to be more
    /// Unity/C# friendly and are mapped from API data models. Specter objects may contain additional
    /// Game specific usage logic, convenience properties, etc.
    /// </summary>
    public abstract class SpecterObject : ISpecterObject
    {
        public string Uuid;
        public string Id;
    }

    /// <summary>
    /// Base class for Specter resources. Specter resources are any in-game entities configured on the Specter dashboard.
    /// This includes entities such as currencies, tasks, items, etc.
    /// </summary>
    public abstract class SpecterResource : SpecterObject
    {
        public string Name;
        public string Description;
        public string IconUrl;
    }

    public enum SpecterResourceLockType
    {
        Progression,
        Item,
        Bundle
    }

    [Serializable]
    public class SpecterUnlockCondition
    {
        public SpecterResourceLockType LockType;
        public string Id;
        public string Name;
        public int? Value;

        public SpecterUnlockCondition() { }

        public SpecterUnlockCondition(SPUnlockConditionResponseData data)
        {
            LockType = data.unlockProgressionSystem != null
                ? SpecterResourceLockType.Progression
                : (data.unlockItem != null ? SpecterResourceLockType.Item : SpecterResourceLockType.Bundle);
            (Id, Name, Value) = LockType switch
            {
                SpecterResourceLockType.Progression => (data.unlockProgressionSystem!.id, data.unlockProgressionSystem.name, data.lockedLevelNo),
                SpecterResourceLockType.Item => (data.unlockItem!.id, data.unlockItem.name, 1),
                SpecterResourceLockType.Bundle => (data.unlockBundle.id, data.unlockBundle.name, 1),
                _ => throw new NotImplementedException($"Unlock condition of type {LockType} not implemented. Please report Specter SDK bug")
            };
        }
    }

}