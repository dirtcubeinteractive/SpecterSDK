using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Leaderboards endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPLeaderboardAttribute : SPEnum<SPLeaderboardAttribute>
    {
        public static readonly SPLeaderboardAttribute Schedule = new SPLeaderboardAttribute(0, "schedule", "Schedule");
        public static readonly SPLeaderboardAttribute PrizeDistribution = new SPLeaderboardAttribute(1, "prizeDistribution", "Prize Distribution");
        public static readonly SPLeaderboardAttribute Meta = new SPLeaderboardAttribute(2, "meta", "Meta");
        public static readonly SPLeaderboardAttribute Tags = new SPLeaderboardAttribute(3, "tags", "Tags");
        
        private SPLeaderboardAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get leaderboards from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetLeaderboardsRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of leaderboard IDs to fetch specific leaderboards.
        /// </summary>
        public List<string> leaderboardIds { get; set; }
        
        /// <summary>
        /// An array of match IDs to filter leaderboards by specific matches.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// A keyword for searching across leaderboard names.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to further filter or annotate leaderboard data.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// An array of schedule statuses to filter leaderboards by timing. Eg usage: SPCompetitionScheduleStatus.InProgress
        /// </summary>
        public List<SPCompetitionStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Specific attributes of leaderboards to include in the response. Eg usage: SPLeaderboardAttribute.PrizeDistribution
        /// </summary>
        public List<SPLeaderboardAttribute> attributes { get; set; }
    }

    public class SPGetLeaderboardsResultV2 : SpecterApiResultBase<SPGetLeaderboardsResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetLeaderboardsResultV2> GetLeaderboardsAsync(SPGetLeaderboardsRequestV2 request)
        {
            var result = await PostAsync<SPGetLeaderboardsResultV2, SPGetLeaderboardsResponse>("/v2/client/app/get-leaderboards", AuthType, request);
            return result;
        }
    }
}