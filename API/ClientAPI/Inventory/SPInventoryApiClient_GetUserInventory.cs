using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    /// <summary>
    /// Represents a request to get the user's inventory from the Specter Inventory API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetUserInventoryRequest"/> class represents a request to get a user's inventory from the Specter Inventory API.
    /// It can be used to specify the filter criteria for the inventory items and bundles to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetItemsFromInventory method in the <see cref="SPInventoryApiClient"/> class to retrieve the user inventory from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserInventoryRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of item IDs used as filter criteria for retrieving inventory items from the Specter Inventory API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The itemIds property is used in the <see cref="SPGetUserInventoryRequest"/> class to specify the specific item IDs for filtering the retrieved inventory items from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique item ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> itemIds { get; set; }
        
        /// <summary>
        /// Represents a list of bundle IDs used as filter criteria for retrieving inventory bundles from the Specter Inventory API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The bundleIds property is used in the SPGetUserInventoryRequest class to specify the specific bundle IDs for filtering the retrieved inventory bundles from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique bundle ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> bundleIds { get; set; }
        
        /// <summary>
        /// Filter to search for and retrieve items and/or bundles by their name.
        /// Multiple items/bundles can be fetched by setting a name substring
        /// <example>
        /// Items named Gold Coin and Silver Coin can be fetched by searching for 'coin' or 'Coin' </example>
        /// </summary>
        public string search { get; set; }

        /// <summary>
        /// Represents the field by which the returned objects should be sorted.
        /// </summary>
        /// <remarks>
        /// The sortField property is used in the <see cref="SPGetUserInventoryRequest"/> class
        /// to specify the field by which the user's inventory should be sorted when retrieved
        /// from the Specter Inventory API.
        /// </remarks>
        /// <example>sortField = "name"</example>
        public string sortField { get; set; }

        /// <summary>
        /// Represents the sort order for the user's inventory.
        /// Possible values are "asc" for ascending order and "desc" for descending order.
        /// </summary>
        public string sortOrder { get; set; }
        
        /// <summary>
        /// The collection ID from which to retrieve the items and/or bundles. See <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/>
        /// for information about the collection ID
        /// </summary>
        public string collectionId { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetUserInventoryAsync method in the SPInventoryApiClient class.
    /// </summary>
    public class SPGetUserInventoryResult : SpecterApiResultBase<SPGetUserInventoryResponseData>
    {
        // List of inventory items fetched.
        public List<SpecterInventoryItem> Items;
        
        // List of inventory bundles fetched.
        public List<SpecterInventoryBundle> Bundles;

        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterInventoryItem>();
            foreach (var itemData in Response.data.items)
                Items.Add(new SpecterInventoryItem(itemData));

            Bundles = new List<SpecterInventoryBundle>();
            foreach (var bundleData in Response.data.bundles)
                Bundles.Add(new SpecterInventoryBundle(bundleData));
        }
    }

    public partial class SPInventoryApiClient
    {
        /// <summary>
        /// Get the user inventory asynchronously.
        /// </summary>
        /// <remarks>
        /// For full information about the get user inventory endpoint, see the Inventory section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetUserInventoryRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetUserInventoryResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetUserInventoryResult> GetUserInventoryAsync(SPGetUserInventoryRequest request)
        {
            var result = await PostAsync<SPGetUserInventoryResult, SPGetUserInventoryResponseData>("/v1/client/inventory/get-inventory", AuthType, request);
            return result;
        }
    }
}
