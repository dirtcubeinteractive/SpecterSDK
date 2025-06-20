using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Tournament History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTournamentHistoryAttribute : SPEnum<SPTournamentHistoryAttribute>
    {
        public static readonly SPTournamentHistoryAttribute Match = new SPTournamentHistoryAttribute(0, nameof(Match).ToLower(), nameof(Match));
        public static readonly SPTournamentHistoryAttribute SourceType = new SPTournamentHistoryAttribute(1, "sourceType", "Source Type");
        public static readonly SPTournamentHistoryAttribute Config = new SPTournamentHistoryAttribute(2, nameof(Config).ToLower(), nameof(Config));
        public static readonly SPTournamentHistoryAttribute Type = new SPTournamentHistoryAttribute(4, "type", "Type");
        
        private SPTournamentHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get tournament history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyTournamentHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to fetch tournaments.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter tournaments.
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTournamentHistoryAttribute> attributes { get; set; }
    }

    public class SPGetMyTournamentHistoryResult : SpecterApiResultBase<SPGetMyTournamentHistoryResponse>
    {
        public List<SPTournamentHistoryEntry> Tournaments { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Tournaments = Response.data == null ? new List<SPTournamentHistoryEntry>() : Response.data.ConvertAll(x => new SPTournamentHistoryEntry(x));
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyTournamentHistoryResult> GetTournamentHistory(SPGetMyTournamentHistoryRequest request)
        {
            var result = await PostAsync<SPGetMyTournamentHistoryResult, SPGetMyTournamentHistoryResponse>("/v2/client/player/me/get-tournament-history", AuthType, request);
            return result;
        }
    }
}