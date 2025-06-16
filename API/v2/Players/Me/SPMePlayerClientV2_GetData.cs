using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to get player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// An optional array of keys to fetch specific player data.
        /// </summary>
        public List<string> keys { get; set; }
    }

    public class SPGetMyPlayerDataResult : SpecterApiResultBase<SPGetMyPlayerDataResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyPlayerDataResult> GetDataAsync(SPGetMyPlayerDataRequest request)
        {
            var result = await PostAsync<SPGetMyPlayerDataResult, SPGetMyPlayerDataResponse>("/v2/client/player/me/get-data", AuthType, request);
            return result;
        }
    }
}