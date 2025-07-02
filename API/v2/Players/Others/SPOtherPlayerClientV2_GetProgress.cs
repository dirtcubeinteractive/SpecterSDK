using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get progression marker progress for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerProgressRequest : SPApiRequestBase
    {
        /// <summary>
        /// The ID of the target user.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Array of progression marker IDs to retrieve. Required field when fetching
        /// another player's progress.
        /// </summary>
        public List<string> progressionMarkerIds { get; set; }
    }
    
    public class SPGetOtherPlayerProgressResult : SpecterApiResultBase<SPGetOtherPlayerProgressResponse>
    {
        public List<SPMarkerProgress> MarkerProgressList { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MarkerProgressList = Response.data?.ConvertAll(x => new SPMarkerProgress(x));
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerProgressResult> GetProgressAsync(SPGetOtherPlayerProgressRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerProgressResult, SPGetOtherPlayerProgressResponse>("/v2/client/player/get-progress", AuthType, request);
            return result;
        }
    }
}