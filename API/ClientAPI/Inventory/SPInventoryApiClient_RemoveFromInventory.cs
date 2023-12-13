using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemoveFromInventoryRequest : SPApiRequestBase
    {
        public List<SPInventoryApiClient.SPInventoryEntityInfo> bundles { get ; set ; }
        public List<SPInventoryApiClient.SPInventoryEntityInfo> items { get ; set; }
    }

    public class SPRemoveFromInventoryResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }

    }

    public partial class SPInventoryApiClient
    {
        public async Task<SPRemoveFromInventoryResult> RemoveFromInventoryAsync(SPRemoveFromInventoryRequest request)
        {
            var result = await PostAsync<SPRemoveFromInventoryResult, SPGeneralResponseData>("/v1/client/inventory/remove", AuthType, request);
            return result;
        }
    }
 

}
