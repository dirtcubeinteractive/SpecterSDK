using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents a request to get items from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetItemsRequest class represents a request to get items from the Specter App API.
    /// It can be used to specify the filter criteria for the items to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetItemsAsync method in the SPAppApiClient class to retrieve the items from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetItemsRequest : SPPaginatedApiRequest, IAttributeConfigurable
    {
        /// <summary>
        /// Represents a list of item IDs used as filter criteria for retrieving items from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The itemIds property is used in the SPGetItemsRequest class to specify the specific item IDs for filtering the retrieved items from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique item ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> itemIds { get; set; }
        
        /// <summary>
        /// Additional attributes of the retrieved items that you wish to receive (eg: createdAt, updatedAt etc.)
        /// </summary>
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// Specify this value if you wish to retrieve only items that have lock conditions
        /// (eg: locked by progression systems, locked by item, etc. see <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/economy/items/items-configuration#access-and-eligibility">Access & Eligibility</a> for more info)
        /// </summary>
        public bool? isLocked { get; set; }
        
        /// <summary>
        /// Filter to search for and retrieve items by their name.
        /// Multiple items can be fetched by setting a name substring
        /// <example>
        /// Items named Gold Coin and Silver Coin can be fetched by searching for 'coin' or 'Coin' </example>
        /// </summary>
        public string search { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetItemsAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetItemsResult : SpecterApiResultBase<SPGetItemsResponseData>
    {
        // List of all items fetched
        public List<SpecterItem> Items;
        
        // Total count of items configured on the dashboard
        public int TotalItemCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterItem>();
            foreach (var itemData in Response.data.items)
            {
                Items.Add(new SpecterItem(itemData));
            }
            TotalItemCount = Response.data.totalCount;
        }
    }


    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of items asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetItemsRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetItemsResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetItemsResult> GetItemsAsync(SPGetItemsRequest request)
        {
            var result = await PostAsync<SPGetItemsResult, SPGetItemsResponseData>("/v1/client/app/get-items", AuthType, request);
            return result;
        }
    }

}
