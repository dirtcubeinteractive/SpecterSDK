using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Matches endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPMatchAttribute : SPEnum<SPMatchAttribute>
    {
        public static readonly SPMatchAttribute Leaderboards = new SPMatchAttribute(0, "leaderboards", "Leaderboards");
        public static readonly SPMatchAttribute Competitions = new SPMatchAttribute(1, "competitions", "Competitions");
        public static readonly SPMatchAttribute Meta = new SPMatchAttribute(2, "meta", "Meta");
        public static readonly SPMatchAttribute Tags = new SPMatchAttribute(3, "tags", "Tags");
        
        private SPMatchAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to retrieve data related to matches within the app.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchesRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// Optional array of match IDs to fetch specific matches.
        /// </summary>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// Optional array of game IDs to filter matches by game.
        /// </summary>
        public List<string> gameIds { get; set; }
        
        /// <summary>
        /// Search keyword for filtering matches by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Tags to filter the matches.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of matches to include in the response. Eg usage: SPMatchAttribute.Leaderboards
        /// </summary>
        public List<SPMatchAttribute> attributes { get; set; }
    }

    public class SPGetMatchesResultV2 : SpecterApiResultBase<SPGetMatchesResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetMatchesResultV2> GetMatchesAsync(SPGetMatchesRequestV2 request)
        {
            var result = await PostAsync<SPGetMatchesResultV2, SPGetMatchesResponse>("/v2/client/app/get-matches", AuthType, request);
            return result;
        }
    }
}