using System;
using Newtonsoft.Json;
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
}