using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.User
{
    /// <summary>
    /// Represents a request to remove player data.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemovePlayerDataRequest: SPApiRequestBase
    {
        /// <summary>
        /// A list of keys to remove from player data.
        /// </summary>
        public List<string> keysToRemove;
    }

    /// <summary>
    /// Represents the result of a player data removal operation.
    /// </summary>
    public class SPRemovePlayerDataResult : SpecterApiResultBase<SPRemovePlayerDataResponseData>
    {
        // A dictionary representing the remaining player data.
        public Dictionary<string, SPPlayerData> PlayerDataDict;

        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    public partial class SPUserApiClient
    {
        /// <summary>
        /// Removes the requested keys from player data asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPRemovePlayerDataRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPRemovePlayerDataResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPRemovePlayerDataResult> RemovePlayerData(SPRemovePlayerDataRequest request)
        {
            var result = await PostAsync<SPRemovePlayerDataResult, SPRemovePlayerDataResponseData>("/v1/client/user/remove-player-data", AuthType, request);
            return result;
        }
    }
}
