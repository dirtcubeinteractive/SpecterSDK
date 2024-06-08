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

    public class SPGetTournamentRankingsResult : SpecterApiResultBase<SPCompetitionResultResponseData>
    {
        public SpecterCompetitionResult TournamentRankings;

        protected override void InitSpecterObjectsInternal()
        {
            TournamentRankings = new SpecterCompetitionResult(Response.data);
        }
    }

    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetTournamentRankingsResult> GetTournamentRankingsAsync(
            SPGetTournamentRankingsRequest request)
        {
            var result =
                await PostAsync<SPGetTournamentRankingsResult, SPCompetitionResultResponseData>(
                    "/v1/client/competitions/get-tournament-rankings", AuthType, request);
            return result;
        }
    }
}