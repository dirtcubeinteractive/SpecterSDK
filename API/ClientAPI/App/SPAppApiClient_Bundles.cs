using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetBundlesRequest : SPApiRequestBaseData
    {
        public List<string> itemIds { get; set; }

        public int? offset { get; set; }

        public int? limit { get; set; }

    }

    public class SPGetBundlesResult : SpecterApiResultBase<SPBundleResponseDataList>
    {
        public List<SpecterBundle> Bundles;
        protected override void InitSpecterObjectsInternal()
        {
            Bundles = new List<SpecterBundle>();
            foreach (var itemData in Response.data)
            {
                Bundles.Add(new SpecterBundle(itemData));
            }
        }
    }


    public partial class SPAppApiClient
    {
        // public async Task<SPGetBundlesRsult> GetBundlesAsync(SPGetItemsRequest request)
        // {
        //     var result = await PostAsync<SPGetBundlesRsult, SPBundleResponseDataList>("/v1/client/app/get-bundles", AuthType, request);
        //     return result;
        // }
    }

}
