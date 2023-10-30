using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserInventoryRequest : SPApiRequestBaseData
    {
        public int? offset { get; set; }
        public int? limit { get; set; }
        public string search { get; set; }
        public string sortField { get; set; }
        public string collectionId { get; set; }
        public string sortOrder { get; set; }
        public string itemId { get; set; }
        public string bundleId { get; set; }
    }

    public class SPGetItemsFromInventoryResult : SpecterApiResultBase<SPUserInventoryResponseData>
    {
        public SpectorUserInventoryItem spectorUserInventoryItem;
        protected override void InitSpecterObjectsInternal()
        {
            spectorUserInventoryItem = new SpectorUserInventoryItem(Response.data);
        }
    }

    public partial class SPInventoryApiClient
    {
        public async Task<SPGetItemsFromInventoryResult> GetItemsFromInventory(SPGetUserInventoryRequest request)
        {
            var result = await PostAsync<SPGetItemsFromInventoryResult, SPUserInventoryResponseData>("/v1/client/inventory/get-inventory", AuthType, request);
            return result;
        }
    }
}
