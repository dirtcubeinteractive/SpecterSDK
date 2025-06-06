using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

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
}