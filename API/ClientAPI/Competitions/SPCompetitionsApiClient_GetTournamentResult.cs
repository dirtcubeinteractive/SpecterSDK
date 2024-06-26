using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using System;
using System.Threading.Tasks;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPGetTournamentResultRequest : SPPaginatedApiRequest
    {
        public string competitionId { get; set; }
        public string instanceId { get; set; }
    }

    public class SPGetTournamentResultData : SpecterApiResultBase<SPLeaderboardRankingsResponseData>
    {
        public SpecterLeaderboardRankings TournamentResult;

        protected override void InitSpecterObjectsInternal()
        {
            TournamentResult = new SpecterLeaderboardRankings(Response.data);
        }
    }

    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetTournamentResultData> GetTournamentResultAsync(SPGetTournamentResultRequest request)
        {
            var result = await PostAsync<SPGetTournamentResultData, SPLeaderboardRankingsResponseData>("/v1/client/competitions/get-tournament-result", AuthType, request);
            return result;
        }
    }
}