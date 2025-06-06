using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.User
{
    /// <summary>
    /// Represents a request to update player data.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdatePlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// List of player data to be updated. The server will simply add the provided keys and values
        /// specified in each <see cref="SPPlayerDataUnit"/> if they don't exist, or update the values if they do. 
        /// </summary>
        public List<SPPlayerDataUnit> playerData { get; set; }
        
        /// <summary>
        /// The permission to set for the list of player data units. Public permissions allow clients to
        /// retrieve and update the data at runtime. Private data can currently only be viewed and updated on the dashboard once set.
        /// </summary>
        public SPPlayerDataPermission permission { get; set; }
    }

    /// <summary>
    /// Represents the result of updating player data.
    /// </summary>
    public class SPUpdatePlayerDataResult : SpecterApiResultBase<SPUpdatePlayerDataResponseData>
    {
        // The update player data
        public Dictionary<string, SPPlayerData> PlayerDataDict;
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    /// <summary>
    /// Represents a single key value pairing of player data to send in the update request.
    /// </summary>
    [Serializable]
    public class SPPlayerDataUnit
    {
        /// <summary>
        /// The key for the player data value
        /// </summary>
        public string key { get; set; }
        
        /// <summary>
        /// A serializable type to set as a value for the given key.
        /// </summary>
        public object value { get; set; }
    }

    public partial class SPUserApiClient
    {
        /// <summary>
        /// Updates the custom player data asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPUpdatePlayerDataRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPUpdatePlayerDataResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPUpdatePlayerDataResult> UpdatePlayerData(SPUpdatePlayerDataRequest request)
        {
            var result = await PostAsync<SPUpdatePlayerDataResult, SPUpdatePlayerDataResponseData>("/v1/client/user/update-player-data", AuthType, request);
            return result;
        }
    }
}