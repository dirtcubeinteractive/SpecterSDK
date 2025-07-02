using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to update player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateMyPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of key-value pairs representing player data to be updated or added.
        /// </summary>
        public List<SPPlayerDataKeyValue> playerData { get; set; }
        
        /// <summary>
        /// Optional permission level for the data being updated.
        /// Possible values are 'private' or 'public'
        /// </summary>
        public string permission { get; set; }
    }

    public class SPUpdateMyPlayerDataResult : SpecterApiResultBase<SPUpdateMyPlayerDataResponse>
    {
        public Dictionary<string, SPPlayerDataEntry> PlayerDataDict { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data?.ToDictionary(kvp => kvp.Key, kvp => new SPPlayerDataEntry(kvp.Value)) ?? new Dictionary<string, SPPlayerDataEntry>();
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPUpdateMyPlayerDataResult> UpdatePlayerDataAsync(SPUpdateMyPlayerDataRequest request)
        {
            var result = await PostAsync<SPUpdateMyPlayerDataResult, SPUpdateMyPlayerDataResponse>("/v2/client/player/me/update-data", AuthType, request);
            return result;
        }
    }
}