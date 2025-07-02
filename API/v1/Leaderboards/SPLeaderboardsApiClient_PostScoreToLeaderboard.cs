using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.API.v1.Matches;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Leaderboards
{
    /// <summary>
    /// Represents the request to post a user's score to a specified leaderboard.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPostScoreToLeaderboardRequest : SPApiRequestBase
    {
        /// <summary>
        /// IDs of the leaderboards where the score is to be posted.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
        
        /// <summary>
        /// The score obtained by the player.
        /// </summary>
        public int score { get; set; }
    }

    [Serializable]
    public class SPPostScoreResult : SpecterApiResultBase<SPGeneralListResponseData>
    {
        /// <summary>
        /// This response returns a simple success message, so no specific object is expected.
        /// </summary>
        public List<object> ObjectList;
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectList = Response.data;
        }
    }

    public partial class SPLeaderboardsApiClient
    {
        /// <summary>
        /// Sends a request to the Specter server to post the player's score to a specified leaderboard asynchronously.
        /// <remarks>
        /// This route is meant to be used to post a score to custom leaderboards that you configure on the dashboard. For leaderboards associated with matches and competitions,
        /// the score is posted when you make the <see cref="SPMatchesApiClient.EndMatchSessionAsync"/> call in the <see cref="SPMatchesApiClient"/>
        /// </remarks>
        /// </summary>
        /// <param name="request">An instance of <see cref="SPPostScoreToLeaderboardRequest"/> which encapsulates the data for the score to be posted.</param> 
        /// <returns>The result of the post operation.</returns>
        public async Task<SPPostScoreResult> PostScoreToLeaderboardAsync(SPPostScoreToLeaderboardRequest request)
        {
            var result = await PostAsync<SPPostScoreResult, SPGeneralListResponseData>("/v1/client/leaderboards/post-score", AuthType, request);
            return result;
        }
    }
}