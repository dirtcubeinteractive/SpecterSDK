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
    public class SPGetStoresRequest : SPPaginatedApiRequest
    {
        public List<string> storeIds;
    }

    public class SPGetStoresResult : SpecterApiResultBase<SPStoreResponseDataList>
    {
        public List<SpecterStore> Stores;
        protected override void InitSpecterObjectsInternal()
        {
            Stores = new();
            foreach (var store in Response.data)
            {
                Stores.Add(new(store));
            }
        }
    }

    public partial class SPStoreApiClient
    {
        public async Task<SPGetStoresResult> GetStoresAsync(SPGetStoresRequest request)
        {
            var result = await PostAsync<SPGetStoresResult, SPStoreResponseDataList>("/v1/client/stores/get-stores", AuthType, request);
            return result;
        }
    }
}
