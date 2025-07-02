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
    /// Represents a request to remove player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveOtherPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of keys representing the data fields to be removed from the player's profile.
        /// </summary>
        public List<string> keysToRemove { get; set; }
    }

    public class SPRemoveOtherPlayerDataResult : SpecterApiResultBase<SPRemoveOtherPlayerDataResponse>
    {
        public Dictionary<string, SPPlayerDataEntry> PlayerDataDict { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data?.ToDictionary(kvp => kvp.Key, kvp => new SPPlayerDataEntry(kvp.Value)) ?? new Dictionary<string, SPPlayerDataEntry>();
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPRemoveOtherPlayerDataResult> RemoveDataAsync(SPRemoveOtherPlayerDataRequest request)
        {
            var result = await PostAsync<SPRemoveOtherPlayerDataResult, SPRemoveOtherPlayerDataResponse>("/v2/client/player/remove-data", AuthType, request);
            return result;
        }
    }
}