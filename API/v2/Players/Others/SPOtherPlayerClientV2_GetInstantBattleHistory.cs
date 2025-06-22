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
    /// Represents a request to get instant battle history for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerInstantBattleHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
        
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
    
    public class SPGetOtherPlayerInstantBattleHistoryResult : SpecterApiResultBase<SPGetOtherPlayerInstantBattleHistoryResponse>
    {
        public List<SPInstantBattleHistoryEntry> InstantBattles { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            InstantBattles = Response.data == null ? new List<SPInstantBattleHistoryEntry>() : Response.data.ConvertAll(x => new SPInstantBattleHistoryEntry(x));
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerInstantBattleHistoryResult> GetInstantBattleHistoryAsync(SPGetOtherPlayerInstantBattleHistoryRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerInstantBattleHistoryResult, SPGetOtherPlayerInstantBattleHistoryResponse>("/v2/client/player/get-instant-battle-history", AuthType, request);
            return result;
        }
    }
}