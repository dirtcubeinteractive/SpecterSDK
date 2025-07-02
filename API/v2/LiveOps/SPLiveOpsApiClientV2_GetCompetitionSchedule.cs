using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.LiveOps
{
    /// <summary>
    /// Represents a request to retrieve schedule history for specified competitions.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCompetitionScheduleRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier of the competition for which to retrieve schedule history.
        /// </summary>
        public string competitionId { get; set; }
    }

    public class SPGetCompetitionScheduleResult : SpecterApiResultBase<SPGetCompetitionScheduleResponse>
    {
        public SPSchedule Schedule { get; set; }
        public SPCompetitionResource Competition { get; set; }
        public List<SPInstanceSchedule> Instances { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Schedule = new SPSchedule(Response.data);
            Competition = new SPCompetitionResource(Response.data.competition);
            Instances  = Response.data.instances?.ConvertAll(x => new SPInstanceSchedule(x)) ?? new List<SPInstanceSchedule>();
        }
    }

    public partial class SPLiveOpsApiClientV2
    {
        public async Task<SPGetCompetitionScheduleResult> GetCompetitionScheduleAsync(SPGetCompetitionScheduleRequest request)
        {
            var result = await PostAsync<SPGetCompetitionScheduleResult, SPGetCompetitionScheduleResponse>("/v2/client/liveops/get-competition-schedule-history", AuthType, request);
            return result;
        }
    }
}