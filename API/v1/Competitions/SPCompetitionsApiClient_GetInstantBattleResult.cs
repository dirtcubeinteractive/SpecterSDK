using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Competitions
{
    [Serializable]
    public class SPGetInstantBattleResultRequest : SPPaginatedApiRequest
    {
        public string competitionId { get; set; }
        public string entryId { get; set; }
    }

    public class SPGetInstantBattleResultData : SpecterApiResultBase<SPLeaderboardRankingsResponseData>
    {
        public SpecterLeaderboardRankings InstantBattleResult;
        protected override void InitSpecterObjectsInternal()
        {
            InstantBattleResult = new SpecterLeaderboardRankings(Response.data);
        }
    }

    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetInstantBattleResultData> GetInstantBattleResultAsync(SPGetInstantBattleResultRequest request)
        {
            var result = await PostAsync<SPGetInstantBattleResultData, SPLeaderboardRankingsResponseData>("/v1/client/competitions/get-instantbattle-result", AuthType, request);
            return result;
        }
    }
}