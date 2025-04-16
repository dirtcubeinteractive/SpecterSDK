using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Tournament History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTournamentHistoryAttribute : SPEnum<SPTournamentHistoryAttribute>
    {
        public static readonly SPTournamentHistoryAttribute Match = new SPTournamentHistoryAttribute(0, nameof(Match).ToLower(), nameof(Match));
        public static readonly SPTournamentHistoryAttribute Type = new SPTournamentHistoryAttribute(1, nameof(Type).ToLower(), nameof(Type));
        public static readonly SPTournamentHistoryAttribute SourceType = new SPTournamentHistoryAttribute(2, "sourceType", "Source Type");
        public static readonly SPTournamentHistoryAttribute Config = new SPTournamentHistoryAttribute(3, nameof(Config).ToLower(), nameof(Config));
        public static readonly SPTournamentHistoryAttribute RankingMethod = new SPTournamentHistoryAttribute(4, "rankingMethod", "Ranking Method");
        
        private SPTournamentHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get tournament history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTournamentHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to fetch tournaments.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter tournaments.
        /// </summary>
        public List<SPCompetitionStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTournamentHistoryAttribute> attributes { get; set; }
    }
}