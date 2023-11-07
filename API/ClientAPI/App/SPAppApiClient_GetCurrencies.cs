using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCurrenciesRequest : SPApiRequestBaseData
    {
        public List<string> currencyIds { get; set; } 
        public SPCurrencyType type { get; set; }
        public string search { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    [Serializable]
    public class SPGetCurrenciesResult : SpecterApiResultBase<SPCurrencyResponseDataList>
    {
        public List<SpecterCurrency> Currencies;
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = new();
            foreach (var currency in Response.data)
            {
                Currencies.Add(new SpecterCurrency(currency));
            }
        }   
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetCurrenciesResult> GetCurrenciesAsync(SPGetCurrenciesRequest request)
        {
            var result = await PostAsync<SPGetCurrenciesResult, SPCurrencyResponseDataList>("/v1/client/app/get-currencies",AuthType,request);
            return result;
        }
    }
}

