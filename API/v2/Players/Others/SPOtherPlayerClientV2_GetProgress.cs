using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get progression marker progress for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerProgressRequest : SPApiRequestBase
    {
        /// <summary>
        /// The ID of the target user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Array of progression marker IDs to retrieve.
        /// </summary>
        public List<string> progressionMarkerIds { get; set; }
    }
}