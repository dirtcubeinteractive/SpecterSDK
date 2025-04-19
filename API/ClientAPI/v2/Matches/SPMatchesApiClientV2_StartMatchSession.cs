using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Matches
{
    /// <summary>
    /// Represents a request to start a previously created match session.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPStartMatchSessionRequest : SPApiRequestBase
    {
        /// <summary>
        /// A unique identifier for the match session.
        /// </summary>
        public string matchSessionId { get; set; }
        
        /// <summary>
        /// The identifier of the match to start the session for.
        /// </summary>
        public string matchId { get; set; }
        
        /// <summary>
        /// Identifier for the competition associated with the match session.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// An array containing details of users participating in the session.
        /// </summary>
        public List<SPMatchUserInfoV2> userInfo { get; set; }
    }
}