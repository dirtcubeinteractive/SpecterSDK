using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCompetitionsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Filter to retrieve specific competitions by their Id.
        /// </summary>
        public List<string> competitionIds {  get; set; }
        
        /// <summary>
        /// Filter to retrieve specific competitions by associated games if configured for a game match in the dashboard.
        /// </summary>
        public List<string> gameIds { get; set; }

        /// <summary>
        /// Filter retrieved competition by schedule statuses. See <see cref="SPScheduleStatus"/> for possible values
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Filter to retrieve only certain types of competitions. See <see cref="SPCompetitionFormat"/> for possible values
        /// </summary>
        public List<SPCompetitionFormat> formatTypes { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }
        
        public List<SPApiRequestEntity> entities { get; set; }
        public List<string> attributes { get; set; }
    }


    public class SPGetCompetitionsResult : SpecterApiResultBase<SPGetCompetitionsResponseData>
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
        public async Task<SPGetCompetitionsResult> GetCompetitionsAsync(SPGetCompetitionsRequest request)
        {
            var result = await PostAsync<SPGetCompetitionsResult, SPGetCompetitionsResponseData>("/v1/client/app/get-competitions", AuthType, request);
            return result;
        }
    }


}
