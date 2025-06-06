using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    /// <summary>
    /// Represents info about an item in the inventory that should be consumed.
    /// Includes the amount to be consumed and identifiers to locate the item within the inventory.
    /// </summary>
    [Serializable]
    public class SPConsumeItemInfo : ISpecterEventConfigurable
    {
        /// <summary>
        /// The ID for the item configured on the dashboard.
        /// </summary>
        public string id;
        
        /// <summary>
        /// The amount of the item to be consumed.
        /// </summary>
        public int amount;
        
        /// <summary>
        /// The unique instance id within the inventory of the item. This field is optional, used only if you wish to consume a
        /// specific instance of the item in the inventory.
        /// </summary>
        public string instanceId;
        
        /// <summary>
        /// A collection ID which you define in your code when adding the item to inventory. The collection concept is simply to allow
        /// developers to group inventory items according to the needs of the game.
        /// </summary>
        /// <example>
        /// If your game allows players to buy several kinds of vehicles, you may have collection IDs like <b>bikes_collection</b>, <b>cars_collection</b>, <b>trucks_collection</b>, etc.
        /// </example>
        public string collectionId;
        
        /// <summary>
        /// Dictionary of optional Specter params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
            
        /// <summary>
        /// Dictionary of optional custom params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents a request to consume items in the inventory.
    /// This feature is aimed at facilitating inventory management by adjusting the quantities of items as they are utilized within the application, enhancing the realism and dynamism of user interactions.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPConsumeItemRequest : SPApiRequestBase
    {
        // List of item infos.
        public List<SPConsumeItemInfo> items;
    }

    /// <summary>
    /// Represents the result of a consume item operation.
    /// </summary>
    public class SPConsumeItemResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        /// <summary>
        /// This response returns a simple success message, so no specific object is expected.
        /// </summary>
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
    
    public partial class SPInventoryApiClient
    {
        /// <summary>
        /// Sends a request to the Specter server to consume specified items from the inventory.
        /// </summary>
        /// <param name="request">Contains the details of the items to be consumed. See <see cref="SPConsumeItemRequest"/> for the structure of the request object.</param> 
        /// <returns>A Task representing the operation of consuming items from the inventory. The result will specifically be a <see cref="SPConsumeItemResult"/> object.</returns>
        public async Task<SPConsumeItemResult> ConsumeItems(SPConsumeItemRequest request)
        {
            var result = await PostAsync<SPConsumeItemResult, SPGeneralResponseData>("/v1/client/user/inventory/consume-item", AuthType, request);
            return result;
        }
    }
}
