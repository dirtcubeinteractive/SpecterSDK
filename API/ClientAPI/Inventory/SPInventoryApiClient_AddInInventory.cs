using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPAddInInventoryRequest : SPApiRequestBaseData , IInventoryRequestConfigurable
    {
        public List<Bundle> bundles { get; set; }
        public List<Item> items { get; set; }
    }

    public class SPAddItemsInInventoryResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
    public partial class SPInventoryApiClient
    {
        public async Task<SPAddItemsInInventoryResult> AddItemsInInventory(SPAddInInventoryRequest request)
        {
            var result = await PostAsync<SPAddItemsInInventoryResult, SPGeneralResponseData>("/v1/client/inventory/add", AuthType, request);
            return result;
        }
    }
}
