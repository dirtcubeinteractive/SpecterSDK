using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Instant Battles endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPInstantBattleAttribute : SPEnum<SPInstantBattleAttribute>
    {
        public static readonly SPInstantBattleAttribute Schedule = new SPInstantBattleAttribute(0, "schedule", "Schedule");
        public static readonly SPInstantBattleAttribute UnlockConditions = new SPInstantBattleAttribute(1, "unlockConditions", "Unlock Conditions");
        public static readonly SPInstantBattleAttribute PrizeDistribution = new SPInstantBattleAttribute(2, "prizeDistribution", "Prize Distribution");
        public static readonly SPInstantBattleAttribute EntryFees = new SPInstantBattleAttribute(3, "entryFees", "Entry Fees");
        public static readonly SPInstantBattleAttribute Meta = new SPInstantBattleAttribute(4, "meta", "Meta");
        public static readonly SPInstantBattleAttribute Tags = new SPInstantBattleAttribute(5, "tags", "Tags");
        
        private SPInstantBattleAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get instant battles from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetInstantBattlesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to filter instant battles.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of match IDs to narrow down instant battle records.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// A keyword for searching instant battle titles.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to further refine or annotate instant battle data.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// An array of schedule statuses for filtering instant battles. Eg usage: SPCompetitionScheduleStatus.InProgress
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes of instant battles to include in the response. Eg usage: SPInstantBattleAttribute.PrizeDistribution
        /// </summary>
        public List<SPInstantBattleAttribute> attributes { get; set; }
    }

    public class SPGetInstantBattlesResult : SpecterApiResultBase<SPGetInstantBattlesResponse>
    {
        public List<SPInstantBattle> InstantBattles { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            InstantBattles = Response.data?.instantbattle == null ? new List<SPInstantBattle>() : Response.data.instantbattle.ConvertAll(x => new SPInstantBattle(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetInstantBattlesResult> GetInstantBattlesAsync(SPGetInstantBattlesRequest request)
        {
            var result = await PostAsync<SPGetInstantBattlesResult, SPGetInstantBattlesResponse>("/v2/client/app/get-instant-battles", AuthType, request);
            return result;
        }
    }
}