using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to remove player data.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveMyPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of keys representing the data fields to be removed from the player's profile.
        /// </summary>
        public List<string> keysToRemove { get; set; }
    }

    public class SPRemoveMyPlayerDataResult : SpecterApiResultBase<SPRemoveMyPlayerDataResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPRemoveMyPlayerDataResult> RemoveDataAsync(SPRemoveMyPlayerDataRequest request)
        {
            var result = await PostAsync<SPRemoveMyPlayerDataResult, SPRemoveMyPlayerDataResponse>("/v2/client/player/me/remove-data", AuthType, request);
            return result;
        }
    }
}