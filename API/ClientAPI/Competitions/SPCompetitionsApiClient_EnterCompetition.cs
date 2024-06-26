using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPEnterCompetitionRequest : SPApiRequestBase, ISpecterEventConfigurable
    {
        public string competitionId { get; set; }
        public Dictionary<string, object> specterParams { get; set; }
        public Dictionary<string, object> customParams { get; set; }
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