using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Stores
{
    /// <summary>
    /// Represents a request to get the store categories info from the Specter Stores API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetStoresCategoriesRequest class represents a request to get categories info for a specified store from the Specter Stores API.
    /// It can be used to specify the filter criteria for the categories to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetStoreCategoriesAsync method in the <see cref="SPStoreApiClient"/> class to retrieve the categories from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoresCategoriesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The configured ID of the store from which to retrieve categories.
        /// </summary>
        public string storeId { get; set; }
        
        /// <summary>
        /// List of category IDs to filter the retrieved categories by.
        /// </summary>
        public List<string> categoryIds { get; set; }

        /// <summary>
        /// Filter to search for and retrieve categories by their name.
        /// Multiple categories can be fetched by setting a name substring
        /// <example>
        /// Categories named 'Avatar Upper Body' and 'Avatar Lower Body' can be fetched by searching for 'avatar' or 'Avatar' </example>
        /// </summary>
        public string search { get; set; }

        /// <summary>
        /// Represents the field by which the returned objects should be sorted.
        /// </summary>
        /// <remarks>
        /// The sortField property is used in the <see cref="SPGetStoresCategoriesRequest"/> class
        /// to specify the field by which the categories should be sorted when retrieved
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
    /// Represents the result of the GetStoreCategoriesAsync method in the SPStoreApiClient class.
    /// </summary>
    public class SPGetStoresCategoriesResult : SpecterApiResultBase<SPStoreCategoryResponseDataList>
    {
        // List of categories fetched.
        public List<SpecterStoreCategory> StoreCategories;
        
        protected override void InitSpecterObjectsInternal()
        {
            StoreCategories = new();
            foreach (var storeCategory in Response.data)
                StoreCategories.Add(new(storeCategory));
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
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetStoresCategoriesRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetStoresCategoriesResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetStoresCategoriesResult> GetStoreCategoriesAsync(SPGetStoresCategoriesRequest request)
        {
            var result = await PostAsync<SPGetStoresCategoriesResult, SPStoreCategoryResponseDataList>("/v1/client/stores/get-categories", AuthType, request);
            return result;
        }
    }
}
