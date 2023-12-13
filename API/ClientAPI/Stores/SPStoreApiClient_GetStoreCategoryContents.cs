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
    public class SPGetStoresCategoryContentsRequest : SPApiRequestBase
    {
        public string storeId;
        public string categoryId;
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SPApiRequestEntity> entities;
    }

    public class SPGetStoresCategoryContentsResult : SpecterApiResultBase<SPStoreCategoryContentResponseData>
    {
        public List<SpecterStoreItemInfo> Items;
        public List<SpecterStoreBundleInfo> Bundles;
        public List<SpecterStoreCurrencyInfo> Currencies;
        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterStoreItemInfo>();
            foreach (var item in Response.data.items)
                Items.Add(new SpecterStoreItemInfo(item));
            
            Bundles = new List<SpecterStoreBundleInfo>();
            foreach (var bundle in Response.data.bundles)
                Bundles.Add(new SpecterStoreBundleInfo(bundle));
            
            Currencies = new List<SpecterStoreCurrencyInfo>();
            foreach (var currency in Response.data.currencies)
                Currencies.Add(new SpecterStoreCurrencyInfo(currency));
        }
    }

    public partial class SPStoreApiClient
    {
        public async Task<SPGetStoresCategoryContentsResult> GetStoreCategoryContentsAsync(SPGetStoresCategoryContentsRequest request)
        {
            var result = await PostAsync<SPGetStoresCategoryContentsResult, SPStoreCategoryContentResponseData>("/v1/client/stores/get-contents", AuthType, request);
            return result;
        }
    }
}
