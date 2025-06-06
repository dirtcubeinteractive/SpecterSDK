using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's progression marker progress.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetProgressRequest : SPApiRequestBase
    {
        /// <summary>
        /// An array of progression marker IDs to fetch user progress.
        /// </summary>
        public List<string> progressionMarkerIds { get; set; }
    }
}