using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEquipUnequipItemInfo
    {
        public string slotId;
        public string id;
        public bool shouldEquip;
        public int? amount;
    }

    [Serializable]
    public class SPEquipOrUnEquipRequest : SPApiRequestBase
    {
        public List<SPEquipUnequipItemInfo> items;
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
        public async Task<SPEquipOrUnEquipResult> EquipOrUnEquipItems(SPEquipOrUnEquipRequest request)
        {
            var result = await PostAsync<SPEquipOrUnEquipResult, SPGeneralResponseData>("/v1/client/inventory/equip-unequip", AuthType, request);
            return result;
        }
    }
}
