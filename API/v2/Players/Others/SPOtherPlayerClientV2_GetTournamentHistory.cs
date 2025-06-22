using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get tournament history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerTournamentHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// ID of the player to retrieve tournament history for.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// An array of competition IDs to fetch tournaments.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Array of schedule statuses to filter tournaments.
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPTournamentHistoryAttribute> attributes { get; set; }
    }
    
    public class SPGetOtherPlayerTournamentHistoryResult : SpecterApiResultBase<SPGetOtherPlayerTournamentHistoryResponse>
    {
        public List<SPTournamentHistoryEntry> Tournaments { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Tournaments = Response.data == null ? new List<SPTournamentHistoryEntry>() : Response.data.ConvertAll(x => new SPTournamentHistoryEntry(x));
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerTournamentHistoryResult> GetTournamentHistory(SPGetOtherPlayerTournamentHistoryRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerTournamentHistoryResult, SPGetOtherPlayerTournamentHistoryResponse>("/v2/client/player/get-tournament-history", AuthType, request);
            return result;
        }
    }
}