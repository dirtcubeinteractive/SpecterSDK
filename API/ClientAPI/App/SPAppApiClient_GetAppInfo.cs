using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetAppInfoRequest : SPApiRequestBase
    {
        public List<string> attributes { get; set; }
    }

    public class SPGetAppInfoResult : SpecterApiResultBase<SPAppInfoResponseData>
    {
        public SpecterApp App { get; private set; }

        protected override void InitSpecterObjectsInternal()
        {
            App = new SpecterApp(Response.data);
        }
    }
    
    public partial class SPAppApiClient
    {
        public async Task<SPGetAppInfoResult> GetAppInfoAsync(SPGetAppInfoRequest request)
        {
            var result = await PostAsync<SPGetAppInfoResult, SPAppInfoResponseData>("/v1/client/app/get-info", AuthType, request);
            return result;
        }
    }
}