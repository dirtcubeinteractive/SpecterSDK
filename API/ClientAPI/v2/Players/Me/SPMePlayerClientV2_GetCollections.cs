using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's collections.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCollectionsRequest : SPPaginatedApiRequest
    {
        // All required parameters are inherited from SPPaginatedApiRequest
        // (limit and offset for pagination)
    }
}