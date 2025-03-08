using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPGameAttributes
    {
        public const string HowTo = "howTo";
        public const string ScreenshotUrls = "screenshotUrls";
        public const string VideoUrls = "videoUrls";
        public const string IsScreenOrientationLandscape = "isScreenOrientationLandscape";
        public const string Genre = "genre";
        public const string Tags = "tags";
        public const string Meta  = "meta";
        public const string Platforms = "platforms";
        public const string Locations = "locations";
        public const string Matches = "matches";
    }
    
    /// <summary>
    /// Represents a request to get games available within the application.
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
    }
}