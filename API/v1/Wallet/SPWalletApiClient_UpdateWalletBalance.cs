using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Interfaces;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Wallet
{
    /// <summary>
    /// Represents a request to update a currency amount for a user in the Specter SDK.
    /// </summary>
    [Serializable]
    public class SPUpdateWalletBalanceRequest : SPApiRequestBase 
    {
        /// <summary>
        /// The amount to update the currency by.
        /// </summary>
        public float amount { get; set; }
        
        /// <summary>
        /// The dashboard specified ID of the currency.
        /// </summary>
        public string currencyId { get; set; }
        
        /// <summary>
        /// The operation for the update. See <see cref="SPOperations"/> for possible values.
        /// </summary>
        public SPOperations operation { get; set; }
        
        /// <summary>
        /// Dictionary of optional Specter params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
            
        /// <summary>
        /// Dictionary of optional custom params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents the result of updating the wallet balance.
    /// </summary>
    public class SPUpdateWalletBalanceResult : SpecterApiResultBase<SPWalletCurrencyResponseData>
    {
        // The updated wallet currency with the new balance.
        public SpecterWalletCurrency WalletCurrency;

        protected override void InitSpecterObjectsInternal()
        {
            WalletCurrency = new SpecterWalletCurrency(Response.data);
        }
    }

    public partial class SPWalletApiClient
    {
        /// <summary>
        /// Update the wallet currency amounts of a user asynchronously.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For full information about the update wallet balance endpoint, see the Wallet section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>.
        /// </para>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPUpdateWalletBalanceRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPUpdateWalletBalanceResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPUpdateWalletBalanceResult> UpdateWalletBalanceAsync(SPUpdateWalletBalanceRequest request)
        {
            var result = await PostAsync<SPUpdateWalletBalanceResult, SPWalletCurrencyResponseData>("/v1/client/wallet/update-balance", AuthType, request);;
            return result;
        }
    }
}
