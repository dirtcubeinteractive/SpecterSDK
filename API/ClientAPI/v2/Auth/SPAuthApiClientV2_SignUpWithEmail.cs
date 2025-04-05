using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Auth
{
    /// <summary>
    /// Represents a request to register a new user with an email and password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPSignUpWithEmailRequestV2 : SPApiRequestBase
    {
        /// <summary>
        /// User's email address.
        /// </summary>
        public string email { get; set; }
        
        /// <summary>
        /// Password for the account.
        /// </summary>
        public string password { get; set; }
        
        /// <summary>
        /// Referral code for sign-up (optional).
        /// </summary>
        public string referralCode { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the user (optional).
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}