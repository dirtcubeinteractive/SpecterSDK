using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Leaderboards
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPostScoreToLeaderboardRequest : SPApiRequestBase
    {
        public string leaderboardId { get; set; }
        public int score { get; set; }
    }

    [Serializable]
    public class SPPostScoreResult : SpecterApiResultBase<SPGeneralListResponseData>
    {
        public List<object> ObjectList;
        protected override void InitSpecterObjectsInternal()
        {
            ObjectList = Response.data;
        }
    }

    public partial class SPLeaderboardsApiClient
    {
        public async Task<SPPostScoreResult> PostScoreToLeaderboardAsync(SPPostScoreToLeaderboardRequest request)
        {
            var result = await PostAsync<SPPostScoreResult, SPGeneralListResponseData>("/v1/client/leaderboards/post-score", AuthType, request);
            return result;
        }
    }
}