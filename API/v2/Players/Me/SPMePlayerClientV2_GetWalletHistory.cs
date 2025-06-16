using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's wallet transaction history.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyWalletHistoryRequest : SPPaginatedApiRequest
    {
        // All required parameters are inherited from SPPaginatedApiRequest
    }

    public class SPGetMyWalletHistoryResult : SpecterApiResultBase<SPGetMyWalletHistoryResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyWalletHistoryResult> GetWalletHistoryAsync(SPGetMyWalletHistoryRequest request)
        {
            var result = await PostAsync<SPGetMyWalletHistoryResult, SPGetMyWalletHistoryResponse>("/v2/client/player/me/get-wallet-history", AuthType, request);
            return result;
        }
    }
}