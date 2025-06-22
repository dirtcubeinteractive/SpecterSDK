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
    /// Represents a request to get instant battle history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyInstantBattleHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of competition IDs to fetch instant battles.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter instant battles.
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPInstantBattleHistoryAttribute> attributes { get; set; }
    }

    public class SPGetMyInstantBattleHistoryResult : SpecterApiResultBase<SPGetMyInstantBattleHistoryResponse>
    {
        public List<SPInstantBattleHistoryEntry> InstantBattles { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            InstantBattles = Response.data == null ? new List<SPInstantBattleHistoryEntry>() : Response.data.ConvertAll(x => new SPInstantBattleHistoryEntry(x));
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyInstantBattleHistoryResult> GetInstantBattleHistoryAsync(SPGetMyInstantBattleHistoryRequest request)
        {
            var result = await PostAsync<SPGetMyInstantBattleHistoryResult, SPGetMyInstantBattleHistoryResponse>("/v2/client/player/me/get-instant-battle-history", AuthType, request);
            return result;
        }
    }
}