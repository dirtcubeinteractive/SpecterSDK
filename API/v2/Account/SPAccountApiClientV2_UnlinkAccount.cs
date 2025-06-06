using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to unlink an account from the user's profile.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUnlinkAccountRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specifies the type of account to unlink, such as a custom ID, Google, or Facebook account.
        /// </summary>
        public SPAccountAuthProvider type { get; set; }
    }
}