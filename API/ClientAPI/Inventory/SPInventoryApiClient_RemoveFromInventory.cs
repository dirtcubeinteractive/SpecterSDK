using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    /// <summary>
    /// This class represents a request to remove items or bundles from a user's inventory in the Specter 
    /// inventory management system. For more details, refer to the Inventory section in the <a href="https://doc.specterap.xyz">Specter API Docs</a>.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveFromInventoryRequest : SPApiRequestBase
    {
        /// <summary>
        /// A list of <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> objects that contain information about bundles to remove from a player's inventory.
        /// </summary>
        public List<SPInventoryApiClient.SPInventoryEntityInfo> bundles { get; set; }
        
        /// <summary>
        /// A list of <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> objects that contain information about items to remove from a player's inventory.
        /// </summary>
        public List<SPInventoryApiClient.SPInventoryEntityInfo> items { get; set; }
    }

    /// <summary>
    /// Represents the result of removing an item or bundle from a user's inventory in the SPInventoryApiClient.
    /// </summary>
    public class SPRemoveFromInventoryResult : SpecterApiResultBase<SPGeneralResponseData>
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
        /// Submits a request to remove items or bundles from a user's inventory, and returns the result of the operation.  
        /// </summary>
        /// <param name="request">An instance of <see cref="SPRemoveFromInventoryRequest"/> which encapsulates the data for the items or bundles to be added.</param> 
        /// <returns>A Task representing the operation of adding items or bundles to the inventory. The result will specifically be a <see cref="SPRemoveFromInventoryResult"/> object.</returns>
        public async Task<SPRemoveFromInventoryResult> RemoveFromInventoryAsync(SPRemoveFromInventoryRequest request)
        {
            var result = await PostAsync<SPRemoveFromInventoryResult, SPGeneralResponseData>("/v1/client/inventory/remove", AuthType, request);
            return result;
        }
    }
 

}
