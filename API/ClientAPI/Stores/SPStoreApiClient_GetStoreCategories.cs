using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Stores
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoresCategoriesRequest : SPPaginatedApiRequest
    {
        public string storeId;
        public List<string> categoryIds;
    }

    public class SPGetStoresCategoriesResult : SpecterApiResultBase<SPStoreCategoryResponseDataList>
    {
        public List<SpecterStoreCategory> StoreCategories;
        protected override void InitSpecterObjectsInternal()
        {
            StoreCategories = new();
            foreach (var storeCategory in Response.data)
                StoreCategories.Add(new(storeCategory));
        }
    }

    public partial class SPStoreApiClient
    {
        public async Task<SPGetStoresCategoriesResult> GetStoreCategoriesAsync(SPGetStoresCategoriesRequest request)
        {
            var result = await PostAsync<SPGetStoresCategoriesResult, SPStoreCategoryResponseDataList>("/v1/client/stores/get-categories", AuthType, request);
            return result;
        }
    }
}
