using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Competitions
{
    /// <summary>
    /// Represents a request to check attempts for a competition entry.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCheckAttemptsRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the entry to check attempts for.
        /// </summary>
        public string entryId { get; set; }
    }
}