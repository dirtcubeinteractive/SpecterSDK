using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v1.Competitions
{
    [Serializable]
    public class SPPostScoreToTournamentRequest : SPApiRequestBase
    {
        public string entryId { get; set; }
        public int score { get; set; }
    }

    public class SPPostScoreToTournamentResult : SpecterApiResultBase<SPGeneralListResponseData>
    {
        public List<object> ObjectList { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectList = Response.data;
        }
    }
    
    public partial class SPCompetitionsApiClient
    {
        public async Task<SPPostScoreToTournamentResult> PostScoreToTournamentAsync(SPPostScoreToTournamentRequest request)
        {
            var result = await PostAsync<SPPostScoreToTournamentResult, SPGeneralListResponseData>("/v1/client/competitions/post-score-to-tournament", AuthType, request);
            return result;
        }
    }
}