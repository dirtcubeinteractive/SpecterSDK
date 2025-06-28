using System;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.APIModels.ClientModels
{
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