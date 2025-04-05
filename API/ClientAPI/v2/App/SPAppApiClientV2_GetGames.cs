using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Games endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPGameAttribute : SPEnum<SPGameAttribute>
    {
        public static readonly SPGameAttribute HowTo = new SPGameAttribute(0, "howTo", "How To");
        public static readonly SPGameAttribute ScreenshotUrls = new SPGameAttribute(1, "screenshotUrls", "Screenshot URLs");
        public static readonly SPGameAttribute VideoUrls = new SPGameAttribute(2, "videoUrls", "Video URLs");
        public static readonly SPGameAttribute IsScreenOrientationLandscape = new SPGameAttribute(3, "isScreenOrientationLandscape", "Is Screen Orientation Landscape");
        public static readonly SPGameAttribute Platforms = new SPGameAttribute(4, "platforms", "Platforms");
        public static readonly SPGameAttribute Locations = new SPGameAttribute(5, "locations", "Locations");
        public static readonly SPGameAttribute Genre = new SPGameAttribute(6, "genre", "Genre");
        public static readonly SPGameAttribute Meta = new SPGameAttribute(7, "meta", "Meta");
        public static readonly SPGameAttribute Tags = new SPGameAttribute(8, "tags", "Tags");
        public static readonly SPGameAttribute Matches = new SPGameAttribute(9, "matches", "Matches");
        
        private SPGameAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch games available within the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetGamesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of unique game identifiers to fetch specific games.
        /// </summary>
        public List<string> gameIds { get; set; }
        
        /// <summary>
        /// A boolean flag to indicate if only the default game should be fetched.
        /// </summary>
        public bool? isDefault { get; set; }
        
        /// <summary>
        /// A string to search for games by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Tags to filter the Games.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of games to include in the response. Eg usage: SPGameAttribute.HowTo
        /// </summary>
        public List<SPGameAttribute> attributes { get; set; }
    }
}