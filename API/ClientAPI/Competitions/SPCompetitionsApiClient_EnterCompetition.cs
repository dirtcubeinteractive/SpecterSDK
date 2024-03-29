using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPEnterCompetitionRequest : SPApiRequestBase
    {
        public string competitionId { get; set; }
    }

    public class SPEnterCompetitionResult : SpecterApiResultBase<SPEnterCompetitionResponseData>
    {
        public string EntryId { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            EntryId = Response.data.entryId;
        }
    }
    
    public partial class SPCompetitionsApiClient
    {
        public async Task<SPEnterCompetitionResult> EnterCompetitionAsync(SPEnterCompetitionRequest request)
        {
            var result = await PostAsync<SPEnterCompetitionResult, SPEnterCompetitionResponseData>("/v1/client/competitions/enter", AuthType, request);
            return result;
        }
    }
}