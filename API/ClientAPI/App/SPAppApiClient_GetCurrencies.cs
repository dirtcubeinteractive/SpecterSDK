using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using UnityEngine.Serialization;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCurrenciesRequest : SPPaginatedApiRequest
    {
        public List<string> currencyIds { get; set; } 
        public SPCurrencyType type { get; set; }
        public string search { get; set; }
    }
    
    public class SPGetCurrenciesResult : SpecterApiResultBase<SPGetCurrenciesResponseData>
    {
        public List<SpecterCurrency> Currencies;
        public int TotalCurrencyCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = new();
            foreach (var currency in Response.data.currencies)
            {
                Currencies.Add(new SpecterCurrency(currency));
            }
            TotalCurrencyCount = Response.data.totalCount;
        }   
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetCurrenciesResult> GetCurrenciesAsync(SPGetCurrenciesRequest request) 
        {
            var result = await PostAsync<SPGetCurrenciesResult, SPGetCurrenciesResponseData>("/v1/client/app/get-currencies",AuthType,request);
            return result;
        }
    }
}

