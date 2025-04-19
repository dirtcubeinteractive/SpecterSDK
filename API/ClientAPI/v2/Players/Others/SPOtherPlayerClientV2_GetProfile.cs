using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get profile information for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// ID of the player to retrieve profile for.
        /// </summary>
        public string id { get; set; }
    }
}