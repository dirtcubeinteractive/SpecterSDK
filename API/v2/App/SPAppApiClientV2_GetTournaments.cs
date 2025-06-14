using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Tournaments endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTournamentAttribute : SPEnum<SPTournamentAttribute>
    {
        public static readonly SPTournamentAttribute Schedule = new SPTournamentAttribute(0, "schedule", "Schedule");
        public static readonly SPTournamentAttribute PrizeDistribution = new SPTournamentAttribute(1, "prizeDistribution", "Prize Distribution");
        public static readonly SPTournamentAttribute UnlockConditions = new SPTournamentAttribute(2, "unlockConditions", "Unlock Conditions");
        public static readonly SPTournamentAttribute EntryFees = new SPTournamentAttribute(3, "entryFees", "Entry Fees");
        public static readonly SPTournamentAttribute Meta = new SPTournamentAttribute(4, "meta", "Meta");
        public static readonly SPTournamentAttribute Tags = new SPTournamentAttribute(5, "tags", "Tags");
        
        private SPTournamentAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get tournaments from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTournamentsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to fetch specific tournaments.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of match IDs to filter tournaments by specific matches.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// A search keyword to filter tournaments by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags for filtering tournaments.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter tournaments by timing. Eg usage: SPCompetitionScheduleStatus.InProgress
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes of tournaments to include in the response. Eg usage: SPTournamentAttribute.PrizeDistribution
        /// </summary>
        public List<SPTournamentAttribute> attributes { get; set; }
    }

    public class SPGetTournamentsResult : SpecterApiResultBase<SPGetTournamentsResponse>
    {
        public List<SPTournament> Tournaments { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Tournaments = Response.data?.tournaments == null ? new List<SPTournament>() : Response.data.tournaments.ConvertAll(x => new SPTournament(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetTournamentsResult> GetTournamentsAsync(SPGetTournamentsRequest request)
        {
            var result = await PostAsync<SPGetTournamentsResult, SPGetTournamentsResponse>("/v2/client/app/get-tournaments", AuthType, request);
            return result;
        }
    }
}