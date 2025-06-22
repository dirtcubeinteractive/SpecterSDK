using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
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
        private List<SPWalletHistoryEntry> Transactions { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Transactions = Response.data?.ConvertAll(x => new SPWalletHistoryEntry(x)) ?? new List<SPWalletHistoryEntry>();
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