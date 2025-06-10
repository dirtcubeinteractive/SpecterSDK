using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Stores
{
    /// <summary>
    /// Represents a request to get the contents for a specific category in a store from the Specter Stores API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetStoresCategoryContentsRequest class represents a request to get contents info for a specified category of a store from the Specter Stores API.
    /// It can be used to specify the filter criteria for the contents details to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the <b>GetStoreCategoryContentsAsync</b> method in the <see cref="SPStoreApiClient"/> class to retrieve the contents details from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    public class SPGetStoresCategoryContentsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// The configured ID of the store from which to retrieve categories.
        /// </summary>
        public string storeId;
        
        /// <summary>
        /// The configured ID of the category within the store ID from which to retrieve contents details.
        /// </summary>
        public string categoryId;
        
        /// <summary>
        /// Optional entities configuration to retrieve only a specific type of contents.
        /// See the Get Store Category contents route in the Stores section of the
        /// <a href="https://doc.specterapp.xyz">Specter API Doc</a> for info about possible entities.
        /// <example>
        /// If you only want to retrieve items details from the store category you would use the "items" entity.
        /// </example>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SPApiRequestEntity> entities;
    }

    /// <summary>
    /// Represents the result of the GetStoreCategoryContentsAsync method in the SPStoreApiClient class.
    /// </summary>
    public class SPGetStoresCategoryContentsResult : SpecterApiResultBase<SPStoreCategoryContentResponseData>
    {
        // List of infos about store items in the requested category.
        public List<SpecterStoreItemInfo> Items;
        
        // List of infos about store bundles in the requested category.
        public List<SpecterStoreBundleInfo> Bundles;

        // Total count of items in the category.
        public int TotalItemsCount;
        
        // Total count of bundles in the category.
        public int TotalBundlesCount;

        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterStoreItemInfo>();
            foreach (var item in Response.data.items)
                Items.Add(new SpecterStoreItemInfo(item));
            
            Bundles = new List<SpecterStoreBundleInfo>();
            foreach (var bundle in Response.data.bundles)
                Bundles.Add(new SpecterStoreBundleInfo(bundle));

            TotalItemsCount = Response.data.totalItemsCount;
            TotalBundlesCount = Response.data.totalBundlesCount;
        }
    }

    public partial class SPStoreApiClient
    {
        /// <summary>
        /// Get the store category contents asynchronously.
        /// </summary>
        /// <remarks>
        /// For full information about the get store category contents endpoint, see the Stores section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetStoresCategoryContentsRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetStoresCategoryContentsResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetStoresCategoryContentsResult> GetStoreCategoryContentsAsync(SPGetStoresCategoryContentsRequest request)
        {
            var result = await PostAsync<SPGetStoresCategoryContentsResult, SPStoreCategoryContentResponseData>("/v1/client/stores/get-contents", AuthType, request);
            return result;
        }
    }
}
