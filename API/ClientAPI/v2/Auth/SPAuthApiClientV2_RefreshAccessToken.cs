using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Auth
{
    /// <summary>
    /// Represents a request to refresh an access token using an entity token and an expiring token.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRefreshAccessTokenRequest : SPApiRequestBase
    {
        /// <summary>
        /// Token identifying the entity.
        /// </summary>
        public string entityToken { get; set; }
        
        /// <summary>
        /// The access token that is about to expire.
        /// </summary>
        public string expiringAccessToken { get; set; }
    }
}