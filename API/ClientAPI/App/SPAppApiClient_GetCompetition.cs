using System.Collections.Generic;
using System;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using System.Threading.Tasks;
using SpecterSDK.ObjectModels;
using Newtonsoft.Json;

namespace SpecterSDK.API.ClientAPI.App
{

    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCompetitionRequest : SPPaginatedApiRequest
    {
        public List<string> competitionIds {  get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
        public List<string> attributes { get; set; }
    }


    public class SPGetCompetitionResult : SpecterApiResultBase<SPGetCompetitionsResponseData>
    {

        public List<SpecterCompetition> Competitions;
        public int TotalCompetitionCount;

        protected override void InitSpecterObjectsInternal()
        {
            Competitions = new List<SpecterCompetition>();

            foreach (var competition in Response.data.competitions)
            {
                Competitions.Add(new SpecterCompetition(competition));
            }

            TotalCompetitionCount = Response.data.totalCount;
        }
    }


    public partial class SPAppApiClient
    {
        public async Task<SPGetCompetitionResult> GetCompetitionAsync(SPGetCompetitionRequest request)
        {
            var result = await PostAsync<SPGetCompetitionResult, SPGetCompetitionsResponseData>("/v1/client/app/get-competitions", AuthType, request);
            return result;
        }
    }


}
