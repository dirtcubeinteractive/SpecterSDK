using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get wallet balance for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerWalletBalanceRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user whose wallet balance is being retrieved.
        /// </summary>
        public string userId { get; set; }
    }
    
    public class SPGetOtherPlayerWalletBalanceResult : SpecterApiResultBase<SPGetOtherPlayerWalletBalanceResponse>
    {
        public List<SPWalletCurrency> Currencies { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = Response.data?.ConvertAll(x => new SPWalletCurrency(x));
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerWalletBalanceResult> GetWalletBalanceAsync(SPGetOtherPlayerWalletBalanceRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerWalletBalanceResult, SPGetOtherPlayerWalletBalanceResponse>("/v2/client/player/get-wallet-balance", AuthType, request);
            return result;
        }
    }
}