using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Auth
{
    /// <summary>
    /// Represents a request to validate an access token.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPValidateTokenRequest : SPApiRequestBase
    {
        /// <summary>
        /// Token identifying the entity (optional).
        /// </summary>
        public string entityToken { get; set; }
        
        /// <summary>
        /// Access token to validate.
        /// </summary>
        public string accessToken { get; set; }
    }
}