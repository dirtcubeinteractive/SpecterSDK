using System;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.APIModels.ClientModels.v1
{
    /// <summary>
    /// Represents the response data for an unlock condition for certain Specter resources like tasks.
    /// See <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/engage/achievements/tasks/task-configuration#access-and-eligibility">Access and Eligibility</a>
    /// section in the Specter user manual.
    /// </summary>
    [Serializable]
    public class SPUnlockConditionResponseData
    {
        // Level number. Null if the condition is not a progression system.
        public int? lockedLevelNo { get; set; }
        
        // Details about the item if the condition is an item lock.
        public SPUnlockResourceData unlockItem { get; set; }
        
        // Details about the bundle if the condition is a bundle lock.
        public SPUnlockResourceData unlockBundle { get; set; }
        
        // Details about the progression system if the condition is a progression system lock.
        public SPUnlockResourceData unlockProgressionSystem { get; set; }
    }

    /// <summary>
    /// Base class for resource response data in the Specter API client models.
    /// This is the minimum amount of information provided in API response when there are
    /// nested objects within the main data object
    /// </summary>
    [Serializable]
    public abstract class SPResourceResponseData : ISpecterApiResponseData, ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
}