using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get collections for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerInventoryCollectionsRequest : SPApiRequestBase
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public string userId { get; set; }
    }
    
    public class SPGetOtherPlayerInventoryCollectionsResult : SpecterApiResultBase<SPGetOtherPlayerInventoryCollectionsResponse>
    {
        public List<string> Collections { get; set; }
        public int TotalCount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Collections = Response.data?.collections ?? new List<string>();
            TotalCount = Response.data?.totalCount ?? 0;
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerInventoryCollectionsResult> GetCollectionsAsync(SPGetOtherPlayerInventoryCollectionsRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerInventoryCollectionsResult, SPGetOtherPlayerInventoryCollectionsResponse>("/v2/client/player/get-collections", AuthType, request);
            return result;
        }
    }
}