using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents a request to get the contents of a specific store category.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoreCategoryContentsRequestV2 : SPPaginatedApiRequest
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

    public class SPGetStoreCategoryContentsResultV2 : SpecterApiResultBase<SPGetStoreCategoryContentsResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetStoreCategoryContentsResultV2> GetStoreCategoryContentsAsync(SPGetStoreCategoryContentsRequestV2 request)
        {
            var result = await PostAsync<SPGetStoreCategoryContentsResultV2, SPGetStoreCategoryContentsResponse>("/v2/client/app/get-store-category-contents", AuthType, request);
            return result;
        }
    }
}