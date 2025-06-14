using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's collections.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyInventoryCollectionsRequest : SPPaginatedApiRequest
    {
        // All required parameters are inherited from SPPaginatedApiRequest
        // (limit and offset for pagination)
    }

    public class SPGetMyInventoryCollectionsResult : SpecterApiResultBase<SPGetMyInventoryCollectionsResponse>
    {
        public List<string> Collections { get; set; }
        public int TotalCount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Collections = Response.data?.collections ?? new List<string>();
            TotalCount = Response.data?.totalCount ?? 0;
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyInventoryCollectionsResult> GetCollectionsAsync(SPGetMyInventoryCollectionsRequest request)
        {
            var result = await PostAsync<SPGetMyInventoryCollectionsResult, SPGetMyInventoryCollectionsResponse>("/v2/client/player/me/get-collections", AuthType, request);
            return result;
        }
    }
}