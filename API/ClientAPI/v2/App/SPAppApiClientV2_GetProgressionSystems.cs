using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Progression Systems endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPProgressionSystemAttribute : SPEnum<SPProgressionSystemAttribute>
    {
        public static readonly SPProgressionSystemAttribute Levels = new SPProgressionSystemAttribute(0, "levels", "Levels");
        public static readonly SPProgressionSystemAttribute Meta = new SPProgressionSystemAttribute(1, "meta", "Meta");
        public static readonly SPProgressionSystemAttribute Tags = new SPProgressionSystemAttribute(2, "tags", "Tags");
        public static readonly SPProgressionSystemAttribute LevelRewardDetails = new SPProgressionSystemAttribute(3, "levels.rewardDetails", "Level Reward Details");
        
        private SPProgressionSystemAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to retrieve progression systems from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetProgressionSystemsRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of progression system IDs to fetch specific progression systems.
        /// </summary>
        public List<string> progressionSystemIds { get; set; }
        
        /// <summary>
        /// An array of progression marker IDs to filter the master data by specific markers.
        /// </summary>
        public List<string> progressionMarkerIds { get; set; }
        
        /// <summary>
        /// A search keyword to filter progression systems by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to further filter the progression systems.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of progression systems to include in the response. Eg usage: SPProgressionSystemAttribute.Levels
        /// </summary>
        public List<SPProgressionSystemAttribute> attributes { get; set; }
    }

    public class SPGetProgressionSystemsResultV2 : SpecterApiResultBase<SPGetProgressionSystemsResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetProgressionSystemsResultV2> GetProgressionSystemsAsync(SPGetProgressionSystemsRequestV2 request)
        {
            var result = await PostAsync<SPGetProgressionSystemsResultV2, SPGetProgressionSystemsResponse>("/v2/client/app/get-progression-systems", AuthType, request);
            return result;
        }
    }
}