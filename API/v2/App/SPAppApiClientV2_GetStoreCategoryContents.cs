using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
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
        public List<SPStoreEntity> Items { get; set; }
        public List<SPStoreEntity> Bundles { get; set; }
        
        public int TotalItemsCount { get; set; }
        public int TotalBundlesCount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = Response.data == null ? new List<SPStoreEntity>() : Response.data.items.ConvertAll(x => new SPStoreEntity(x));
            Bundles = Response.data == null ? new List<SPStoreEntity>() : Response.data.bundles.ConvertAll(x => new SPStoreEntity(x));
            
            TotalItemsCount = Response.data?.totalItemsCount ?? 0;
            TotalBundlesCount = Response.data?.totalBundlesCount ?? 0;
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