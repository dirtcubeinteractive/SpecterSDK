using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Leaderboards
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardDetailsRequest : SPPaginatedApiRequest
    {
        public string leaderboardId { get; set; }
        public string matchId { get; set; }
    }
    
    public class SPGetLeaderboardDetailsResult : SpecterApiResultBase<SPLeaderboardResponseData>
    {
        public SpecterLeaderboard Leaderboard;
        protected override void InitSpecterObjectsInternal()
        {
            Leaderboard = new SpecterLeaderboard(Response.data);
        }
    }

    public partial class SPLeaderboardsApiClient
    {
        public async Task<SPGetLeaderboardDetailsResult> GetLeaderboardDetailsAsync(SPGetLeaderboardDetailsRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardDetailsResult, SPLeaderboardResponseData>("/v1/client/leaderboards/get-details", AuthType, request);
            return result;
        }
    }
}