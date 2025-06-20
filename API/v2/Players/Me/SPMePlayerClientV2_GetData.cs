using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
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
        public Dictionary<string, SPPlayerDataEntry> PlayerDataDict { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data?.ToDictionary(kvp => kvp.Key, kvp => new SPPlayerDataEntry(kvp.Value)) ?? new Dictionary<string, SPPlayerDataEntry>();
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