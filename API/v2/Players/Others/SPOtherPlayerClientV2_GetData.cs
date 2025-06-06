using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get data for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// ID of the player to retrieve data for.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Keys of the player data to retrieve.
        /// </summary>
        public List<string> keys { get; set; }
    }
}