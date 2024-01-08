using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    [Serializable]
    public class SPConsumeItemInfo
    {
        public int amount;
        public string instanceId;
        public string collectionId;
        public string id;
    }

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPConsumeItemRequest : SPApiEventConfigurableRequestBase
    {
        public List<SPConsumeItemInfo> items;
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
        public async Task<SPConsumeItemResult> ConsumeItems(SPConsumeItemRequest request)
        {
            var result = await PostAsync<SPConsumeItemResult, SPGeneralResponseData>("/v1/client/user/inventory/consume-item", AuthType, request);
            return result;
        }
    }
}
