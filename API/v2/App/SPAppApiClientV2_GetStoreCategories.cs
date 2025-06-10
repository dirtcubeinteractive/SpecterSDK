using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents a request to get store categories from a specific store.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoreCategoriesRequestV2 : SPPaginatedApiRequest
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

    public class SPGetStoreCategoriesResultV2 : SpecterApiResultBase<SPGetStoreCategoriesResponse>
    {
        public List<SPStoreCategory> StoreCategories { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            StoreCategories = Response.data == null ? new List<SPStoreCategory>() : Response.data.ConvertAll(x => new SPStoreCategory(x));
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetStoreCategoriesResultV2> GetStoreCategoriesAsync(SPGetStoreCategoriesRequestV2 request)
        {
            var result = await PostAsync<SPGetStoreCategoriesResultV2, SPGetStoreCategoriesResponse>("/v2/client/app/get-store-categories", AuthType, request);
            return result;
        }
    }
}