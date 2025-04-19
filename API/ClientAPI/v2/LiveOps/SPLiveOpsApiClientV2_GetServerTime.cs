using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.LiveOps
{
    /// <summary>
    /// Represents a request to get the current server time, optionally formatted for a specific timezone.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetServerTimeRequest : SPApiRequestBase
    {
        /// <summary>
        /// The timezone to format the server time in.
        /// </summary>
        public string timezone { get; set; }
    }
}