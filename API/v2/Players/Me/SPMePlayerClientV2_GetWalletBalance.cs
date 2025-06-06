using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's wallet balance.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetWalletBalanceRequest : SPPaginatedApiRequest
    {
        // All required parameters are inherited from SPPaginatedApiRequest
    }
}