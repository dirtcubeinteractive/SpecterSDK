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
    /// Represents a request to retrieve schedule history for specified leaderboards.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardScheduleRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Unique identifier of the leaderboard for which to retrieve schedule history.
        /// </summary>
        public string leaderboardId { get; set; }
    }
    
    public class SPGetLeaderboardScheduleResult : SpecterApiResultBase<SPGetLeaderboardScheduleResponse>
    {
        public SPSchedule Schedule { get; set; }
        public SPLeaderboardResource Leaderboard { get; set; }
        public List<SPInstanceSchedule> Instances { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Schedule = new SPSchedule(Response.data);
            Leaderboard = new SPLeaderboardResource(Response.data.leaderboard);
            Instances  = Response.data.instances?.ConvertAll(x => new SPInstanceSchedule(x)) ?? new List<SPInstanceSchedule>();
        }
    }

    public partial class SPLiveOpsApiClientV2
    {
        public async Task<SPGetLeaderboardScheduleResult> GetLeaderboardScheduleAsync(SPGetLeaderboardScheduleRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardScheduleResult, SPGetLeaderboardScheduleResponse>("/v2/client/liveops/get-leaderboard-schedule-history", AuthType, request);
            return result;
        }
    }
}