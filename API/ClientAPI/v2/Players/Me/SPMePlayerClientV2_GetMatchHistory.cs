using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Match History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPMatchHistoryAttribute : SPEnum<SPMatchHistoryAttribute>
    {
        public static readonly SPMatchHistoryAttribute Competition = new SPMatchHistoryAttribute(0, "competition", "Competition");
        public static readonly SPMatchHistoryAttribute PlayerDetails = new SPMatchHistoryAttribute(1, "playerDetails", "Player Details");
        
        private SPMatchHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get match history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The ID of the match session to fetch.
        /// </summary>
        public string matchSessionId { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPMatchHistoryAttribute> attributes { get; set; }
    }
}