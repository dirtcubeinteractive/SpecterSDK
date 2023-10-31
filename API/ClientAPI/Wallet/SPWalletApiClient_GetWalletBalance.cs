using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Wallet
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetWalletBalanceRequest : SPApiRequestBaseData
    {
        public List<string> currencyIds  { get; set; }
    }

    public class SPGetWalletBalanceResult : SpecterApiResultBase<SPWalletCurrencyResponseDataList>
    {
        public List<SpecterWalletCurrency> WalletCurrencies;

        protected override void InitSpecterObjectsInternal()
        {
            WalletCurrencies = new List<SpecterWalletCurrency>();

            foreach (var currencyData in Response.data)
            {
                WalletCurrencies.Add(new SpecterWalletCurrency(currencyData));
            }
        }
    }

    public partial class SPWalletApiClient
    {
        public async Task<SPGetWalletBalanceResult> GetWalletBalanceAsync(SPGetWalletBalanceRequest request)
        {           
            var result = await PostAsync<SPGetWalletBalanceResult, SPWalletCurrencyResponseDataList>("/v1/client/wallet/get-balance", AuthType, request);
            return result;
        }
    }
}
