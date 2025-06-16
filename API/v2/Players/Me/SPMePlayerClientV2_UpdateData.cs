using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a player data key-value pair.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPlayerDataKeyValue
    {
        /// <summary>
        /// Unique key for the data field.
        /// </summary>
        public string key { get; set; }
        
        /// <summary>
        /// The value associated with the key.
        /// </summary>
        public object value { get; set; }
    }
    
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
        protected override void InitSpecterObjectsInternal()
        {
            
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