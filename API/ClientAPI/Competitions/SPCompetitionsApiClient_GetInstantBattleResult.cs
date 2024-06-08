using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using System;
using System.Threading.Tasks;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPGetInstantBattleResultRequest : SPPaginatedApiRequest
    {
        public string competitionId { get; set; }
        public string entryId { get; set; }
    }

    public class SPGetInstantBattleResultData : SpecterApiResultBase<SPCompetitionResultResponseData>
    {
        public SpecterCompetitionResult InstantBattleResult;
        protected override void InitSpecterObjectsInternal()
        {
            InstantBattleResult = new SpecterCompetitionResult(Response.data);
        }
    }

    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetInstantBattleResultData> GetInstantBattleResultAsync(SPGetInstantBattleResultRequest request)
        {
            var result = await PostAsync<SPGetInstantBattleResultData, SPCompetitionResultResponseData>("/v1/client/competitions/get-instantbattle-result", AuthType, request);
            return result;
        }
    }
}