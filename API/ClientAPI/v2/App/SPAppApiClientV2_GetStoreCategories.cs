using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents a request to get store categories from a specific store.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoreCategoriesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier of the store from which to fetch categories (e.g. 'main_store').
        /// </summary>
        public string storeId { get; set; }
        
        /// <summary>
        /// Array of category IDs to fetch specific categories (e.g. ['weapon_cat', 'armor_cat']).
        /// </summary>
        public List<string> categoryIds { get; set; }
        
        /// <summary>
        /// Keyword-based search (e.g. 'potion', 'legendary') across category names.
        /// </summary>
        public string search { get; set; }
    }
}