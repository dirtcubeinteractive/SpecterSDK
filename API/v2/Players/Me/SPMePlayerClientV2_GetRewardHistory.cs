using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get reward history for the player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyRewardHistoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of task IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> taskIds { get; set; }
        
        /// <summary>
        /// An array of task group IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> taskGroupIds { get; set; }
        
        /// <summary>
        /// An array of progression system level IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> levelIds { get; set; }
        
        /// <summary>
        /// An array of leaderboard IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
        
        /// <summary>
        /// An array of competition IDs for which to fetch rewards earned if present.
        /// </summary>
        public List<string> competitionIds { get; set; }
        
        /// <summary>
        /// Filter and fetch rewards earned by status.
        /// </summary>
        public SPRewardClaimStatus status { get; set; }
        
        /// <summary>
        /// Additional attributes to fetch with the reward history.
        /// </summary>
        public List<SPRewardHistoryAttribute> attributes { get; set; }
    }

    public class SPGetMyRewardHistoryResult : SpecterApiResultBase<SPGetMyRewardHistoryResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyRewardHistoryResult> GetRewardHistoryAsync(SPGetMyRewardHistoryRequest request)
        {
            var result = await PostAsync<SPGetMyRewardHistoryResult, SPGetMyRewardHistoryResponse>("/v2/client/player/me/get-reward-history", AuthType, request);
            return result;
        }
    }
}