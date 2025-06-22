using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get wallet transaction history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerWalletHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
    }
    
    public class SPGetOtherPlayerWalletHistoryResult : SpecterApiResultBase<SPGetOtherPlayerWalletHistoryResponse>
    {
        private List<SPWalletHistoryEntry> Transactions { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Transactions = Response.data?.ConvertAll(x => new SPWalletHistoryEntry(x)) ?? new List<SPWalletHistoryEntry>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerWalletHistoryResult> GetWalletHistoryAsync(SPGetOtherPlayerWalletHistoryRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerWalletHistoryResult, SPGetOtherPlayerWalletHistoryResponse>("/v2/client/player/get-wallet-history", AuthType, request);
            return result;
        }
    }
}