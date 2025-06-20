using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
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
        public List<SPRewardHistoryEntry> Entries { get; set; }
        public Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>> EntriesBySource { get; set; }
        public Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>> PendingRewardsBySource { get; set; }
        public Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>> ClaimedRewardsBySource { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Entries = new List<SPRewardHistoryEntry>();
            EntriesBySource = new Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>>();
            PendingRewardsBySource = new Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>>();
            ClaimedRewardsBySource = new Dictionary<SPRewardSourceType, List<SPRewardHistoryEntry>>();

            foreach (var element in Response.data)
            {
                var entry = new SPRewardHistoryEntry(element);
                
                Entries.Add(entry);
                if (!EntriesBySource.ContainsKey(entry.SourceType))
                    EntriesBySource.Add(entry.SourceType, new List<SPRewardHistoryEntry>());
                EntriesBySource[entry.SourceType].Add(entry);

                if (entry.Status == SPRewardClaimStatus.Pending)
                {
                    if (!PendingRewardsBySource.ContainsKey(entry.SourceType))
                        PendingRewardsBySource.Add(entry.SourceType, new List<SPRewardHistoryEntry>());
                    PendingRewardsBySource[entry.SourceType].Add(entry);
                }
                else
                {
                    if (!ClaimedRewardsBySource.ContainsKey(entry.SourceType))
                        ClaimedRewardsBySource.Add(entry.SourceType, new List<SPRewardHistoryEntry>());
                    ClaimedRewardsBySource[entry.SourceType].Add(entry);
                }
            }
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