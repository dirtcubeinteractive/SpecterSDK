using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// An optional array of keys to fetch specific player data.
        /// </summary>
        public List<string> keys { get; set; }
    }
}