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
    /// Represents a request to retrieve custom player from the Specter API.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetPlayerDataRequest : SPApiRequestBase
    {
        /// <summary>
        /// The specific player data keys to retrieve.
        /// </summary>
        public List<string> keys;
        
        /// <summary>
        /// The ID of the user whose data you wish to retrieve. Leave null if requesting data for
        /// the currently logged in user.
        /// </summary>
        public string userId;
    }

    /// <summary>
    /// Represents the result of the GetPlayerData method in the SPUserApiClient class.
    /// </summary>
    public class SPGetPlayerDataResult: SpecterApiResultBase<SPGetPlayerDataResponseData>
    {
        /// <summary>
        /// A dictionary of the custom player data. The player data keys are the keys of the dictionary, and
        /// the player data values are within the <see cref="SPPlayerData"/> objects.
        /// </summary>
        public Dictionary<string, SPPlayerData> PlayerDataDict;

        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    public partial class SPUserApiClient
    {
        /// <summary>
        /// Gets the custom player data from Specter asynchronously.
        /// <remarks>
        /// <para>
        /// Custom player data on Specter is stored as a structure of key value pairs of type [string, object]. The object value
        /// can be any type that is serializable into JSON.
        /// </para>
        /// <para>
        /// Only player data set with Public permission will
        /// be retrieved.
        /// </para>
        /// <para>
        /// For more information about this endpoint see the User section in the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </para>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetPlayerDataRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetPlayerDataResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetPlayerDataResult> GetPlayerData(SPGetPlayerDataRequest request)
        {
            var result = await PostAsync<SPGetPlayerDataResult, SPGetPlayerDataResponseData>("/v1/client/user/get-player-data", AuthType, request);
            return result;
        }
    }
}
