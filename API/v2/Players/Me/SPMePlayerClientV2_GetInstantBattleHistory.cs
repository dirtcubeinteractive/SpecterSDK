using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Instant Battle History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPInstantBattleHistoryAttribute : SPEnum<SPInstantBattleHistoryAttribute>
    {
        public static readonly SPInstantBattleHistoryAttribute Match = new SPInstantBattleHistoryAttribute(0, "match", "Match");
        public static readonly SPInstantBattleHistoryAttribute Config = new SPInstantBattleHistoryAttribute(1, "config", "Configuration");
        public static readonly SPInstantBattleHistoryAttribute Type = new SPInstantBattleHistoryAttribute(2, "type", "Type");
        public static readonly SPInstantBattleHistoryAttribute SourceType = new SPInstantBattleHistoryAttribute(3, "sourceType", "Source Type");
        
        private SPInstantBattleHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get instant battle history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetInstantBattleHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to fetch instant battles.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter instant battles.
        /// </summary>
        public List<SPCompetitionStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPInstantBattleHistoryAttribute> attributes { get; set; }
    }
}