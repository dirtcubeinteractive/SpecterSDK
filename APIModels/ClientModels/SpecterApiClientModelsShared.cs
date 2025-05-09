using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.APIModels.ClientModels
{
    public interface ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    public interface ISpecterPlatformData
    {
        public int id { get; }
        public string name { get; }
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
    
    /// <summary>
    /// The base class for all APIs that fire Specter events server side when called
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class SPApiEventConfigurableRequestBase : SPApiRequestBase, ISpecterEventConfigurable
    {
        // Custom parameters to be sent with the event that you have configured on the dashboard
        public Dictionary<string, object> customParams { get; set; }
        
        // Specter params (eg: matchId) which Specter offers out of the box
        public Dictionary<string, object> specterParams { get; set; }
    }

    /// <summary>
    /// Represents the response data for getting the server time.
    /// </summary>
    [Serializable]
    public class SPGetServerTimeResponseData : ISpecterApiResponseData
    {
        public string abbreviation { get; set; }
        public string clientIp { get; set; }
        public DateTime datetime { get; set; }
        public int dayOfWeek { get; set; }
        public int dayOfYear { get; set; }
        public bool dst { get; set; }
        public string dstFrom { get; set; }
        public int dstOffset { get; set; }
        public string dstUntil { get; set; }
        public int rawOffset { get; set; }
        public string timezone { get; set; }
        public int unixtime { get; set; }
        public DateTime utcDatetime { get; set; }
        public string utcOffset { get; set; }
        public int weekNumber { get; set; }
    }

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
    /// Details about a specific lock condition (i.e. item, bundle or progression system).
    /// </summary>
    [Serializable]
    public class SPUnlockResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Base class for platform data in the Specter API client models.
    /// </summary>
    [Serializable]
    public class SPPlatformBaseData : ISpecterPlatformData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    
    /// <summary>
    /// Class representing platforms for an app in the Specter API client models.
    /// </summary>
    [Serializable]
    public class SPAppPlatformData : SPPlatformBaseData
    {
        public string assetBundleUrl { get; set; }
        public string assetBundleVersion { get; set; }
    }
    
    /// <summary>
    /// Class representing genre data for a game
    /// </summary>
    [Serializable]
    public class SPGameGenreData
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Represents geographical location data. applicable in a any Specter entity that provides info about locational availability
    /// See <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/app/app-configuration#location">App location configuration</a> for an example use case.
    /// </summary>
    [Serializable]
    public class SPLocationData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}