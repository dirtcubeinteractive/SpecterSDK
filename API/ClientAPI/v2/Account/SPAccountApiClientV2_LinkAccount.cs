using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.ClientAPI.v2.Account
{
    /// <summary>
    /// Represents a request to link an account to the user's profile.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPLinkAccountRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specifies the type of account to link, such as a custom ID, Google, or Facebook account.
        /// </summary>
        public SPAccountAuthProvider type { get; set; }
        
        /// <summary>
        /// The unique identifier of the account to be linked, specific to the account type.
        /// </summary>
        public string id { get; set; }
    }
}