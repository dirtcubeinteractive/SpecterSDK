using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents a request to get the contents of a specific store category.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoreCategoryContentsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier of the store that houses the category (e.g. 'main_store').
        /// </summary>
        [JsonRequired]
        public string storeId { get; set; }
        
        /// <summary>
        /// A single category ID to retrieve its contents (e.g. 'weapon_cat').
        /// </summary>
        public string categoryId { get; set; }
    }
}