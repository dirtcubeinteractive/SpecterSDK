using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using System;
using System.Threading.Tasks;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPGetTournamentRankingsRequest : SPPaginatedApiRequest
    {
        public string competitionId { get; set; }
        public string instanceId { get; set; }
    }

    public class SPGetTournamentRankingsResult : SpecterApiResultBase<SPLeaderboardRankingsResponseData>
    {
        public SpecterLeaderboardRankings TournamentRankings;

        protected override void InitSpecterObjectsInternal()
        {
            TournamentRankings = new SpecterLeaderboardRankings(Response.data);
        }
    }

    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetTournamentRankingsResult> GetTournamentRankingsAsync(
            SPGetTournamentRankingsRequest request)
        {
            var result =
                await PostAsync<SPGetTournamentRankingsResult, SPLeaderboardRankingsResponseData>(
                    "/v1/client/competitions/get-tournament-rankings", AuthType, request);
            return result;
        }
    }
}