using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get the player's progression marker progress.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyProgressRequest : SPApiRequestBase
    {
        /// <summary>
        /// An array of progression marker IDs to fetch user progress.
        /// </summary>
        public List<string> progressionMarkerIds { get; set; }
    }

    public class SPGetMyProgressResult : SpecterApiResultBase<SPGetMyProgressResponse>
    {
        public List<SPMarkerProgress> MarkerProgressList { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            MarkerProgressList = Response.data?.ConvertAll(x => new SPMarkerProgress(x));
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyProgressResult> GetProgressAsync(SPGetMyProgressRequest request)
        {
            var result = await PostAsync<SPGetMyProgressResult, SPGetMyProgressResponse>("/v2/client/player/me/get-progress", AuthType, request);
            return result;
        }
    }
}