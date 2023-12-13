using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Wallet
{
    [Serializable]
    public class SPUpdateWalletBalanceRequest : SPApiRequestBase 
    {
        public float amount { get; set; }
        public string currencyId { get; set; }
        public SPOperations operation { get; set; }
    }

    public class SPUpdateWalletBalanceResult : SpecterApiResultBase<SPWalletCurrencyResponseData>
    {
        public SpecterWalletCurrency WalletCurrency;

        protected override void InitSpecterObjectsInternal()
        {
            WalletCurrency = new SpecterWalletCurrency(Response.data);
        }
    }

    public partial class SPWalletApiClient
    {
        public async Task<SPUpdateWalletBalanceResult> UpdateWalletBalanceAsync(SPUpdateWalletBalanceRequest request)
        {
            var result = await PostAsync<SPUpdateWalletBalanceResult, SPWalletCurrencyResponseData>("/v1/client/wallet/update-balance", AuthType, request);;
            return result;
        }
    }
}
