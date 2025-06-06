using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to change the user's password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPChangePasswordRequest : SPApiRequestBase
    {
        /// <summary>
        /// The user's current password.
        /// </summary>
        public string currentPassword { get; set; }
        
        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public string newPassword { get; set; }
    }
}