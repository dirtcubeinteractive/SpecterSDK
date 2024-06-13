using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SpecterSDK.API.ClientAPI.Leaderboards
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardResultRequest : SPPaginatedApiRequest
    {
        public string leaderboardId { get; set; }
        public string matchId { get; set; }
        public string instanceOffset { get; set; }
    }

    public class SPGetLeaderboardResultData : SpecterApiResultBase<SPLeaderboardRankingsResponseData>
    {
        public SpecterLeaderboardRankings LeaderboardResult;

        protected override void InitSpecterObjectsInternal()
        {
            LeaderboardResult = new SpecterLeaderboardRankings(Response.data);
        }
    }

    public partial class SPLeaderboardsApiClient
    {
        public async Task<SPGetLeaderboardResultData> GetLeaderboardResultAsync(SPGetLeaderboardResultRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardResultData, SPLeaderboardRankingsResponseData>("/v1/client/leaderboards/get-result", AuthType, request);
            return result;
        }
    }

}