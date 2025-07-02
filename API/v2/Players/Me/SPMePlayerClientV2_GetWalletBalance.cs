using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's wallet balance.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyWalletBalanceRequest : SPPaginatedApiRequest
    {
        // All required parameters are inherited from SPPaginatedApiRequest
    }

    public class SPGetMyWalletBalanceResult : SpecterApiResultBase<SPGetMyWalletBalanceResponse>
    {
        public List<SPWalletCurrency> Currencies { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = Response.data?.ConvertAll(x => new SPWalletCurrency(x));
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyWalletBalanceResult> GetWalletBalanceAsync(SPGetMyWalletBalanceRequest request)
        {
            var result = await PostAsync<SPGetMyWalletBalanceResult, SPGetMyWalletBalanceResponse>("/v2/client/player/me/get-wallet-balance", AuthType, request);
            return result;
        }
    }
}