using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Wallet
{
    /// <summary>
    /// Represents a request to get the user's currency balances from the Specter Wallet API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetWalletBalanceRequest"/> class represents a request to get a user's currency balances from the Specter Inventory API.
    /// It can be used to specify the filter criteria for the currency wallets to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetWalletBalanceAsync method in the <see cref="SPWalletApiClient"/> class to retrieve the user currency wallets from the API.
    /// </para>
    /// </remarks>
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetWalletBalanceRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of currency IDs used as filter criteria for retrieving wallet currency details from the Specter Wallet API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The currencyIds property is used in the <see cref="SPGetWalletBalanceRequest"/> class to specify the specific currency IDs for filtering the retrieved wallet currency details from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique currency ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> currencyIds  { get; set; }
        
        /// <summary>
        /// Represents the field by which the returned objects should be sorted.
        /// </summary>
        /// <remarks>
        /// The sortField property is used in the <see cref="SPGetWalletBalanceRequest"/> class
        /// to specify the field by which the user's wallet currency details should be sorted when retrieved
        /// from the Specter Wallet API.
        /// </remarks>
        /// <example>sortField = "name"</example>
        public string sortField { get; set; }
        
        /// <summary>
        /// Represents the sort order for the user's wallet currencies.
        /// Possible values are "asc" for ascending order and "desc" for descending order.
        /// </summary>
        public string sortOrder { get; set; }
    }

    /// <summary>
    /// Represents the result of a wallet balance request.
    /// </summary>
    public class SPGetWalletBalanceResult : SpecterApiResultBase<SPWalletCurrencyResponseDataList>
    {
        // List of wallet currencies belonging to the user.
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
        /// <summary>
        /// Get the user wallet balances asynchronously.
        /// </summary>
        /// <remarks>
        /// For full information about the get wallet balance endpoint, see the Wallet section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetWalletBalanceRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetWalletBalanceResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetWalletBalanceResult> GetWalletBalanceAsync(SPGetWalletBalanceRequest request)
        {           
            var result = await PostAsync<SPGetWalletBalanceResult, SPWalletCurrencyResponseDataList>("/v1/client/wallet/get-balance", AuthType, request);
            return result;
        }
    }
}
