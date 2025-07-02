using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Competitions
{
    [Serializable]
    public class SPCheckCompetitionAttemptsRequest : SPApiRequestBase
    {
        public string entryId { get; set; }
    }

    public class SPCheckCompetitionAttemptsResult : SpecterApiResultBase<SPCheckCompetitionAttemptsResponseData>
    {
        public int? NumberOfAttemptsLeft { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            NumberOfAttemptsLeft = Response.data.numberOfAttemptsLeft;
        }
    }
    
    public partial class SPCompetitionsApiClient
    {
        public async Task<SPCheckCompetitionAttemptsResult> CheckAttemptsAsync(SPCheckCompetitionAttemptsRequest request)
        {
            var result = await PostAsync<SPCheckCompetitionAttemptsResult, SPCheckCompetitionAttemptsResponseData>("/v1/client/competitions/check-attempts", AuthType, request);
            return result;
        }
    }
}