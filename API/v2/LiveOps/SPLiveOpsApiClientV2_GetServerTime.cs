using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.LiveOps
{
    /// <summary>
    /// Represents a request to get the current server time, optionally formatted for a specific timezone.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetServerTimeRequest : SPApiRequestBase
    {
        /// <summary>
        /// The timezone to format the server time in. Timezone values can be found in moment js timezone
        /// list <a href="https://gist.github.com/diogocapela/12c6617fc87607d11fd62d2a4f42b02a">here</a>.
        /// </summary>
        public string timezone { get; set; }
    }
}