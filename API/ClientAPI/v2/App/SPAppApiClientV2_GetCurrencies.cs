using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the currency types available in the system.
    /// </summary>
    [Serializable]
    public sealed class SPCurrencyTypeV2 : SPEnum<SPCurrencyTypeV2>
    {
        public static readonly SPCurrencyTypeV2 Virtual = new SPCurrencyTypeV2(0, "virtual", "Virtual");
        public static readonly SPCurrencyTypeV2 Real = new SPCurrencyTypeV2(1, "real", "Real");
        
        private SPCurrencyTypeV2(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents the attributes available for the Currency endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPCurrencyAttribute : SPEnum<SPCurrencyAttribute>
    {
        public static readonly SPCurrencyAttribute Meta = new SPCurrencyAttribute(0, "meta", "Meta");
        public static readonly SPCurrencyAttribute Tags = new SPCurrencyAttribute(1, "tags", "Tags");
        
        private SPCurrencyAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch currencies with optional filters.
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
        /// Filter currencies by a specific type (virtual or real). Eg usage: SPCurrencyTypeV2.Virtual
        /// </summary>
        public SPCurrencyTypeV2 type { get; set; }
        
        /// <summary>
        /// A string to search for currencies by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to filter the currencies by.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of currencies to include in the response. Eg usage: SPCurrencyAttribute.Meta
        /// </summary>
        public List<SPCurrencyAttribute> attributes { get; set; }
    }
}