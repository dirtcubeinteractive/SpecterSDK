using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Competitions
{
    [Serializable]
    public class SPGetMyCompetitionsRequest : SPPaginatedApiRequest
    {
        public List<string> competitionIds { get; set; }
        public List<SPCompetitionStatus> status { get; set; }
    }

    public class SPGetMyCompetitionsResult : SpecterApiResultBase<SPResponseDataList<SPEnteredCompetitionResponseData>>
    {
        public List<SpecterEnteredCompetition> MyCompetions;
        
        protected override void InitSpecterObjectsInternal()
        {
            MyCompetions = new List<SpecterEnteredCompetition>();
            foreach (var comp in Response.data)
            {
                MyCompetions.Add(new SpecterEnteredCompetition(comp));
            }
        }
    }
    
    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetMyCompetitionsResult> GetMyCompetitionsAsync(SPGetMyCompetitionsRequest request)
        {
            var result = await PostAsync<SPGetMyCompetitionsResult, SPResponseDataList<SPEnteredCompetitionResponseData>>("/v1/client/competitions/get-my-competitions", AuthType, request);
            return result;
        }
    }
}