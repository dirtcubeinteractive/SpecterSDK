using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to remove player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemovePlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of keys representing the data fields to be removed from the player's profile.
        /// </summary>
        public List<string> keysToRemove { get; set; }
    }
}