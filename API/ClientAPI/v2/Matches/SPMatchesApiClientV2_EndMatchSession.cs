using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Matches
{
    /// <summary>
    /// Represents a request to end an active match session.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEndMatchSessionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the match session to be ended.
        /// </summary>
        public string matchSessionId { get; set; }
        
        /// <summary>
        /// An array of user objects involved in the session, including their outcomes.
        /// </summary>
        public List<SPEndMatchUserInfoV2> userInfo { get; set; }
    }
}