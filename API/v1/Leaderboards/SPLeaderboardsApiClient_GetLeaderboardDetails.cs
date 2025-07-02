using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Leaderboards
{
    /// <summary>
    /// Represents the request to fetch the details of a specific leaderboard.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Leaderboard details include info about the leaderboard, the current user's results on the leaderboard,
    /// and a list of info about other players' results. See <see cref="SPLeaderboardRankingsResponseData"/> for more details.
    /// </para>
    /// <para>
    /// Includes parameters to specify the leaderboard and optional match identifiers. Only one of the 2 fields is needed.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardDetailsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The leaderboard ID that you want the details for. This ID is configured by you on the dashboard when
        /// creating a leaderboard.
        /// </summary>
        public string leaderboardId { get; set; }
        
        /// <summary>
        /// The match ID (if fetching the leaderboard for a particular match configured on the dashboard).
        /// </summary>
        public string matchId { get; set; }
    }
    
    public class SPGetLeaderboardDetailsResult : SpecterApiResultBase<SPLeaderboardRankingsResponseData>
    {
        // The fetched leaderboard
        public SpecterLeaderboardRankings Leaderboard;
        
        protected override void InitSpecterObjectsInternal()
        {
            Leaderboard = new SpecterLeaderboardRankings(Response.data);
        }
    }

    public partial class SPLeaderboardsApiClient
    {
        /// <summary>
        /// Sends a request to the Specter server to fetch the details of a specified leaderboard asynchronously.
        /// <remarks> This is an essential function for providing players with updated and detailed leaderboard information.</remarks>
        /// </summary>
        /// <param name="request">An instance of <see cref="SPGetLeaderboardDetailsRequest"/> which encapsulates the data for the leaderboard whose details are to be fetched.</param> 
        /// <returns>A Task for the asynchronous operation. The task result is a <see cref="SPGetLeaderboardDetailsResult"/> object</returns>
        public async Task<SPGetLeaderboardDetailsResult> GetLeaderboardDetailsAsync(SPGetLeaderboardDetailsRequest request)
        {
            var result = await PostAsync<SPGetLeaderboardDetailsResult, SPLeaderboardRankingsResponseData>("/v1/client/leaderboards/get-details", AuthType, request);
            return result;
        }
    }
}