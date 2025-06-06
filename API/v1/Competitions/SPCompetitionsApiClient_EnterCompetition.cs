using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Competitions
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