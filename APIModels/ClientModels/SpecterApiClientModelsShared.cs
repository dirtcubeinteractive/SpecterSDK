using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.APIModels.ClientModels
{
    public interface ISpecterResourceData
    {
        /// <summary>
        /// The database identifier for the resource. Only used internally on the Specter backend, NOT for use in APIs
        /// </summary>
        public string uuid { get; set; }

        /// <summary>
        /// The unique identifier for the resource set on the Specter dashboard. Use this property in Specter APIs
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Name of the resource
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description of the resource
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// URL to fetch the icon for the resource set on the Specter dashboard.
        /// </summary>
        public string iconUrl { get; set; }
    }

    public interface ISpecterPlatformData
    {
        /// <summary>
        /// Unique identifier for the platform.
        /// </summary>
        public int id { get; }

        /// <summary>
        /// Name of the platform.
        /// </summary>
        public string name { get; }
    }

    [Serializable]
    public class SPResourceData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

    /// <summary>
    /// User authentication account data in SDK responses
    /// </summary>
    [Serializable]
    public class SPUserAuthAccountData
    {
        public string authProvider { get; set; }
        public string userId { get; set; }
    }

    [Serializable]
    public class SPUserAuthAccountDataV2
    {
        public SPAccountAuthProvider authProvider { get; set; }
        public string userId { get; set; }
    }

    /// <summary>
    /// Represents the response data for getting the server time.
    /// </summary>
    [Serializable]
    public class SPServerTimeData
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
    /// Class representing platforms for an app in the Specter API client models.
    /// </summary>
    [Serializable]
    public class SPAppPlatformData : SPPlatformBaseData
    {
        public string assetBundleUrl { get; set; }
        public string assetBundleVersion { get; set; }
        public string minimumGameVersion { get; set; }
    }

    [Serializable]
    public class SPEventData
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    
    [Serializable]
    public class SPPlayerDataEntryData
    {
        public object value { get; set; }
        public SPPlayerDataPermission permission { get; set; }
    }
}