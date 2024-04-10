using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlasticPipe.Server;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Stores
{
    /// <summary>
    /// Represents a request to get the configured stores from the Specter Stores API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetStoresRequest class represents a request to get stores from the Specter Stores API.
    /// It can be used to specify the filter criteria for the stores to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetStoresAsync method in the <see cref="SPStoreApiClient"/> class to retrieve the stores from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoresRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// List of store IDs to filter the retrieved stores by.
        /// </summary>
        public List<string> storeIds;
        
        /// <summary>
        /// Filter to search for and retrieve stores by their name.
        /// Multiple stores can be fetched by setting a name substring
        /// <example>
        /// Stores named 'Avatar Upper Body Store' and 'Avatar Lower Body Store ' can be fetched by searching for 'avatar' or 'Avatar' </example>
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Represents the field by which the returned objects should be sorted.
        /// </summary>
        /// <remarks>
        /// The sortField property is used in the <see cref="SPGetStoresRequest"/> class
        /// to specify the field by which the stores should be sorted when retrieved
        /// from the Specter Stores API.
        /// </remarks>
        /// <example>sortField = "name"</example>
        public string sortField { get; set; }
        
        /// <summary>
        /// Represents the sort order for the retrieved objects.
        /// Possible values are "asc" for ascending order and "desc" for descending order.
        /// </summary>
        public string sortOrder { get; set; }
    }

    /// <summary>
    /// Represents the result of a GetStoresAsync request.
    /// </summary>
    public class SPGetStoresResult : SpecterApiResultBase<SPGetStoresResponseData>
    {
        // List of stores fetched.
        public List<SpecterStore> Stores;

        public int TotalCount;
        public DateTime? LastUpdated;
        
        protected override void InitSpecterObjectsInternal()
        {
            Stores = new();
            foreach (var store in Response.data.stores)
            {
                Stores.Add(new(store));
            }

            TotalCount = Response.data.totalCount;
            LastUpdated = Response.data.lastUpdate;
        }
    }
    
    public partial class SPStoreApiClient
    {
        /// <summary>
        /// Get the store categories asynchronously.
        /// </summary>
        /// <remarks>
        /// For full information about the get store categories endpoint, see the Stores section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetStoresRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetStoresResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetStoresResult> GetStoresAsync(SPGetStoresRequest request)
        {
            var result = await PostAsync<SPGetStoresResult, SPGetStoresResponseData>("/v1/client/stores/get-stores", AuthType, request);
            return result;
        }
    }
}
