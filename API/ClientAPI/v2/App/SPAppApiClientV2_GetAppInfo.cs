using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the App Info endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPAppInfoAttribute : SPEnum<SPAppInfoAttribute>
    {
        public static readonly SPAppInfoAttribute HowTo = new SPAppInfoAttribute(0, "howTo", "How To");
        public static readonly SPAppInfoAttribute ScreenshotUrls = new SPAppInfoAttribute(1, "screenshotUrls", "Screenshot URLs");
        public static readonly SPAppInfoAttribute VideoUrls = new SPAppInfoAttribute(2, "videoUrls", "Video URLs");
        public static readonly SPAppInfoAttribute Categories = new SPAppInfoAttribute(3, "categories", "Categories");
        public static readonly SPAppInfoAttribute Platforms = new SPAppInfoAttribute(4, "platforms", "Platforms");
        public static readonly SPAppInfoAttribute Locations = new SPAppInfoAttribute(5, "locations", "Locations");
        public static readonly SPAppInfoAttribute Genre = new SPAppInfoAttribute(6, "genre", "Genre");
        public static readonly SPAppInfoAttribute Meta = new SPAppInfoAttribute(7, "meta", "Meta");
        public static readonly SPAppInfoAttribute Tags = new SPAppInfoAttribute(8, "tags", "Tags");
        
        private SPAppInfoAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to retrieve detailed information about the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetAppInfoRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specific attributes of app info to include in the response. Eg usage: SPAppInfoAttribute.HowTo
        /// </summary>
        public List<SPAppInfoAttribute> attributes { get; set; }
    }
}