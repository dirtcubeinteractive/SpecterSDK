using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Interfaces;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Inventory
{
    /// <summary>
    /// Represents information about the item that is to be equipped or unequipped in the inventory.
    /// This includes the identification of the item and the action (equip/unequip) to be performed.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEquipUnequipItemInfo : ISpecterEventConfigurable
    {
        /// <summary>
        /// ID of the item as configured on the dashboard.
        /// </summary>
        public string id;
        
        /// <summary>
        /// Instance ID of the inventory item. This field is optional, used only if you wish to equip or unequip a
        /// specific instance of the item in the inventory.
        /// </summary>
        public string instanceId;
        
        /// <summary>
        /// The operation to be performed.
        /// </summary>
        /// <example>Set to true if the item is to be equipped, and false to unequip.</example>
        public bool shouldEquip;
        
        /// <summary>
        /// Number of the item instance to be equipped
        /// </summary>
        public int? amount;
        
        /// <summary>
        /// Collection ID of the item. See <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> for more details about the collection ID.
        /// </summary>
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
    /// Represents a request to equip or unequip items from the inventory.
    /// </summary>
    [Serializable]
    public class SPEquipOrUnEquipRequest : SPApiRequestBase
    {
        public List<SPEquipUnequipItemInfo> items;
    }

    /// <summary>
    /// Represents the result of an Equip or UnEquip operation in the Specter API.
    /// </summary>
    public class SPEquipOrUnEquipResult : SpecterApiResultBase<SPGeneralResponseData>
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
        /// Sends a request to the Specter server to equip or unequip specified items from the inventory.   
        /// This feature enhances the user experience by providing dynamic management of active items.
        /// Refer to the Inventory section of the <a href="https://doc.specterapp.xyz">Specter API Documentation</a> for more details.
        /// </summary>
        /// <param name="request">An instance of <see cref="SPEquipOrUnEquipRequest"/> which encapsulates the data for the items to be equipped or unequipped.</param> 
        /// <returns>A Task containing for the asynchronous equip or unequip operation. The result of the task is an instance of <see cref="SPEquipOrUnEquipResult"/></returns>
        public async Task<SPEquipOrUnEquipResult> EquipOrUnEquipItems(SPEquipOrUnEquipRequest request)
        {
            var result = await PostAsync<SPEquipOrUnEquipResult, SPGeneralResponseData>("/v1/client/inventory/equip-unequip", AuthType, request);
            return result;
        }
    }
}
