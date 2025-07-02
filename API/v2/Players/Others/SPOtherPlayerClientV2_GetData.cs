using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get data for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// ID of the player to retrieve data for.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Keys of the player data to retrieve.
        /// </summary>
        public List<string> keys { get; set; }
    }
    
    public class SPGetOtherPlayerDataResult : SpecterApiResultBase<SPGetOtherPlayerDataResponse>
    {
        public Dictionary<string, SPPlayerDataEntry> PlayerDataDict { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data?.ToDictionary(kvp => kvp.Key, kvp => new SPPlayerDataEntry(kvp.Value)) ?? new Dictionary<string, SPPlayerDataEntry>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerDataResult> GetDataAsync(SPGetOtherPlayerDataRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerDataResult, SPGetOtherPlayerDataResponse>("/v2/client/player/get-data", AuthType, request);
            return result;
        }
    }
}