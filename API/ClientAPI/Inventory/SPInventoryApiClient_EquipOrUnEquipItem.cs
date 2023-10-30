using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEquipOrUnEquipRequest : SPApiRequestBaseData
    {
        public string collectionId { get; set; }
        public string itemId { get; set; }

        public bool isEquipped { get; set; }
    }

    public class SPEquipOrUnEquipResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }

    public partial class SPInventoryApiClient
    {
        public async Task<SPGetItemsFromInventoryResult> EquipOrUnEquipItems(SPGetUserInventoryRequest request)
        {
            var result = await PostAsync<SPGetItemsFromInventoryResult, SPUserInventoryResponseData>("/v1/client/inventory/equip-unequip", AuthType, request);
            return result;
        }
    }
}
