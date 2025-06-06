using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to register a new user with a custom ID.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPSignUpWithCustomIdRequestV2 : SPApiRequestBase
    {
        /// <summary>
        /// Custom ID for the account.
        /// </summary>
        public string customId { get; set; }
        
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