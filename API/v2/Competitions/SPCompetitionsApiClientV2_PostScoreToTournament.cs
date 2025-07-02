using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to post a score to a tournament.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPostScoreToTournamentRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the competition (optional).
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// The unique identifier of the entry to which the score belongs.
        /// </summary>
        public string entryId { get; set; }
        
        /// <summary>
        /// The score to submit for the entry.
        /// </summary>
        public long score { get; set; }
    }

    public class SPPostScoreToTournamentResult : SpecterApiResultBase<SPPostScoreToTournamentResponse>
    {
        protected override void InitSpecterObjectsInternal() { }
    }
    
    public partial class SPCompetitionsApiClientV2
    {
        public async Task<SPPostScoreToTournamentResult> PostScoreToTournamentAsync(SPPostScoreToTournamentRequest request)
        {
            var result = await PostAsync<SPPostScoreToTournamentResult, SPPostScoreToTournamentResponse>("/v2/client/competitions/post-score-to-tournament", AuthType, request);
            return result;
        }
    }
}