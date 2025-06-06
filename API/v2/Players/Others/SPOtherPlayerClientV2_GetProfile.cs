using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get profile information for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string username { get; set; }
        
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string email { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPPlayerProfileAttribute> attributes { get; set; }
    }
}