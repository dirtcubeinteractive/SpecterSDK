using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using UnityEngine.Serialization;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents a request to get progression systems from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetProgressionSystemsRequest class represents a request to get progression systems from the Specter App API.
    /// It can be used to specify the filter criteria for the progression systems to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetProgressionSystemsAsync method in the SPAppApiClient class to retrieve the progression systems from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetProgressionSystemsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of progression system IDs used as filter criteria for retrieving progression systems from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The progressionSystemIds property is used in the SPGetProgressionSystemsRequest class to specify the specific progression system IDs for filtering the retrieved progression systems from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique progression system ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> progressionSystemIds { get; set; }
        
        /// <summary>
        /// Additional attributes of the retrieved progression systems that you wish to receive (eg: createdAt, updatedAt etc.)
        /// </summary>
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// Additional entities that can be fetched along with the list of progression systems
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
    }
    
    /// <summary>
    /// Represents the result of the GetProgressionSystemsAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetProgressionSystemsResult : SpecterApiResultBase<SPGetProgressionSystemsResponseData>
    {
        // List of all progression systems fetched
        public List<SpecterProgressionSystem> ProgressionSystems;
        
        // Total count of progression systems configured on the dashboard
        public int TotalProgressionSystemCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            ProgressionSystems = new List<SpecterProgressionSystem>();
            foreach (var progressionSystem in Response.data.progressionSystems)
            {
                ProgressionSystems.Add(new SpecterProgressionSystem(progressionSystem));
            }
            TotalProgressionSystemCount = Response.data.totalCount;
        }
    }

    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of progression systems asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetProgressionSystemsRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetProgressionSystemsResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetProgressionSystemsResult> GetProgressionSystemsAsync(SPGetProgressionSystemsRequest request)
        {
            var result = await PostAsync<SPGetProgressionSystemsResult, SPGetProgressionSystemsResponseData>("/v1/client/app/get-progression-systems", AuthType, request);
            return result;
        }
    }
}

