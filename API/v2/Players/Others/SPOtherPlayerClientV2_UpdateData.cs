using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to update player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateOtherPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// ID of the player to update data for.
        /// </summary>
        public string userId { get; set; }
        
        /// <summary>
        /// Array of key-value pairs representing player data to be updated or added.
        /// </summary>
        public List<SPPlayerDataKeyValue> playerData { get; set; }
    }
    
    public class SPUpdateOtherPlayerDataResult : SpecterApiResultBase<SPUpdateOtherPlayerDataResponse>
    {
        public Dictionary<string, SPPlayerDataEntry> PlayerDataDict { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data?.ToDictionary(kvp => kvp.Key, kvp => new SPPlayerDataEntry(kvp.Value)) ?? new Dictionary<string, SPPlayerDataEntry>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPUpdateOtherPlayerDataResult> UpdatePlayerDataAsync(SPUpdateOtherPlayerDataRequest request)
        {
            var result = await PostAsync<SPUpdateOtherPlayerDataResult, SPUpdateOtherPlayerDataResponse>("/v2/client/player/update-data", AuthType, request);
            return result;
        }
    }
}