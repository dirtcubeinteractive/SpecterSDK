using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    public static class SPCurrencyAttributes
    {
        public const string Tags = "tags";
        public const string Meta  = "meta";
    }
    
    /// <summary>
    /// Represents a request to get currencies with optional filters.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCurrenciesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of currency IDs to filter results by.
        /// </summary>
        public List<string> currencyIds { get; set; }
        
        /// <summary>
        /// Filter currencies by a specific type (virtual or real)
        /// </summary>
        public string type { get; set; }
        
        /// <summary>
        /// A string to search for currencies by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to filter the currencies by.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Additional data fields or related entities you can request in the API response
        /// </summary>
        public List<string> attributes { get; set; }
    }
}