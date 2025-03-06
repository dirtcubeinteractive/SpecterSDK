using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    /// <summary>
    /// This class represents a request to add items or bundles to a user's inventory in the Specter 
    /// inventory management system. For more details, refer to the Inventory section in the <a href="https://doc.specterap.xyz">Specter API Docs</a>.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPAddInInventoryRequest : SPApiRequestBase
    {
        /// <summary>
        /// A list of <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> objects that contain information about bundles to add to a player's inventory.
        /// </summary>
        public List<SPInventoryApiClient.SPInventoryEntityInfo> bundles { get; set; }
        
        /// <summary>
        /// A list of <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> objects that contain information about items to add to a player's inventory.
        /// </summary>
        public List<SPInventoryApiClient.SPInventoryEntityInfo> items { get; set; }
    }

    /// <summary>
    /// Represents the result of adding an item or bundle to a user's inventory in the SPInventoryApiClient.
    /// </summary>
    public class SPAddInInventoryResult : SpecterApiResultBase<SPGeneralResponseData>
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
        /// Submits a request to add items or bundles to a user's inventory, and returns the result of the operation.  
        /// </summary>
        /// <param name="request">An instance of <see cref="SPAddInInventoryRequest"/> which encapsulates the data for the items or bundles to be added.</param> 
        /// <returns>A Task representing the operation of adding items or bundles to the inventory. The result will specifically be a <see cref="SPAddInInventoryResult"/> object.</returns>
        public async Task<SPAddInInventoryResult> AddInInventory(SPAddInInventoryRequest request)
        {
            var result = await PostAsync<SPAddInInventoryResult, SPGeneralResponseData>("/v1/client/inventory/add", AuthType, request);
            return result;
        }
    }
}
