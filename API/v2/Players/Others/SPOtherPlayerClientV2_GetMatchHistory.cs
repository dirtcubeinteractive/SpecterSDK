using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get match history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerMatchHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPMatchHistoryAttribute> attributes { get; set; }
    }
}