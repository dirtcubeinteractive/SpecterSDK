using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to authenticate a user using a custom ID.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPLoginWithCustomIdRequest : SPApiRequestBase
    {
        /// <summary>
        /// Custom ID for the account.
        /// </summary>
        public string customId { get; set; }
        
        /// <summary>
        /// Flag to create an account if it doesn't exist.
        /// </summary>
        public bool? createAccount { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the user (optional).
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPLoginWithCustomIdResult
    {
        
    }
}