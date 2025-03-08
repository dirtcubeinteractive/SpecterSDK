using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPMarkerAttributes
    {
        public const string Tags = "tags";
        public const string Meta  = "meta";
        public const string ProgressionSystems = "progressionSystems";
    }
    
    /// <summary>
    /// Represents a request to get progression markers from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMarkersRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of progression marker IDs to fetch specific markers.
        /// </summary>
        public List<string> progressionMarkerId { get; set; }
        
        /// <summary>
        /// A string to search for progression markers by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// A list of tags to filter or augment the progression marker data.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
}