using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPAddInInventoryRequest : SPApiEventConfigurableRequestBase
    {
        public List<SPInventoryApiClient.SPInventoryEntityInfo> bundles { get; set; }
        public List<SPInventoryApiClient.SPInventoryEntityInfo> items { get; set; }
    }

    public class SPAddInInventoryResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
    public partial class SPInventoryApiClient
    {
        public async Task<SPAddInInventoryResult> AddInInventory(SPAddInInventoryRequest request)
        {
            var result = await PostAsync<SPAddInInventoryResult, SPGeneralResponseData>("/v1/client/inventory/add", AuthType, request);
            return result;
        }
    }
}
