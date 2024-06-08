using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardsRequest : SPPaginatedApiRequest
    {
        public List<string> leaderboardIds { get; set; }
        public List<string> matchIds { get; set; }
    }

    public class SPGetLeaderboardsResult : SpecterApiResultBase<SPGetLeaderboardsResponseData>
    {
        public List<SpecterLeaderboardBase> Leaderboards;
        public int TotalLeaderboardCount;

        protected override void InitSpecterObjectsInternal()
        {
            Leaderboards = new List<SpecterLeaderboardBase>();
            foreach (var leaderboard in Response.data.leaderboards)
            {
                Leaderboards.Add(new SpecterLeaderboardBase(leaderboard));
            }
            TotalLeaderboardCount = Response.data.totalCount;
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetLeaderboardsResult> GetLeaderboardsAsync(SPGetLeaderboardsRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardsResult, SPGetLeaderboardsResponseData>("/v1/client/app/get-leaderboards", AuthType, request);
            return result;
        }
    }
}