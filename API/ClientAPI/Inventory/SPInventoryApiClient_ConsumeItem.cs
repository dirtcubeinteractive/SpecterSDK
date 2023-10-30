using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPConsumeItemRequest : SPApiRequestBaseData
    {

        public int quantity { get; set; }
        public string collectionId { get; set; }
        public string itemId { get; set; }

    }

    public class SPConsumeItemResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }

    public partial class SPInventoryApiClient
    {
        public async Task<SPGetItemsFromInventoryResult> ConsumeItems(SPGetUserInventoryRequest request)
        {
            var result = await PostAsync<SPGetItemsFromInventoryResult, SPUserInventoryResponseData>("/v1/client/user/inventory/consume-item", AuthType, request);
            return result;
        }
    }
}
