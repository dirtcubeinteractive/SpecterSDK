using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Networking.Models;
using Newtonsoft.Json;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPAppAttributes
    {
        public const string HowTo = "howTo";
        public const string ScreenshotUrls = "screenshotUrls";
        public const string VideoUrls = "videoUrls";
        public const string Categories = "categories";
        public const string Genre = "genre";
        public const string Tags = "tags";
        public const string Meta  = "meta";
        public const string Platforms = "platforms";
        public const string Locations = "locations";
    }
    
    /// <summary>
    /// Represents a request to get detailed information about the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetAppInfoRequest : SPApiRequestBase
    {
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
    
    public partial class SPAppApiClientV2
    {
        
    }
}