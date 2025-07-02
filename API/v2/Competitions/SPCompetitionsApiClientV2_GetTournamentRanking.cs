using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to get tournament ranking data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTournamentRankingRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The unique identifier for the tournament competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// The unique instance identifier for the tournament.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Attributes to optionally request additional data in the response. See <see cref="SPCompetitionRankingAttribute"/>
        /// </summary>
        public List<SPCompetitionRankingAttribute> attributes { get; set; }
    }

    public class SPGetTournamentRankingsResult : SpecterApiResultBase<SPGetTournamentRankingResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPCompetitionsApiClientV2
    {
        public async Task<SPGetTournamentRankingsResult> GetTournamentRankingsAsync(SPGetTournamentRankingRequest request)
        {
            var result = await PostAsync<SPGetTournamentRankingsResult, SPGetTournamentRankingResponse>("/v2/client/competitions/get-tournament-rankings", AuthType, request);
            return result;
        }
    }
}