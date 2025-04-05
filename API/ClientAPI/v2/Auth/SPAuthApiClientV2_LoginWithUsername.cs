using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Auth
{
    /// <summary>
    /// Represents a request to authenticate a user using username and password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPLoginWithUsernameRequest : SPApiRequestBase
    {
        /// <summary>
        /// Username for the account.
        /// </summary>
        public string username { get; set; }
        
        /// <summary>
        /// Password for the account.
        /// </summary>
        public string password { get; set; }
        
        /// <summary>
        /// Flag to create an account if it doesn't exist.
        /// </summary>
        public bool? createAccount { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the user (optional).
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}