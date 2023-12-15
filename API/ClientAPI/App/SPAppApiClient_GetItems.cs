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
    public class SPGetItemsRequest : SPApiRequestBase, IAttributeConfigurable
    {
        public List<string> itemIds { get; set; }

        public List<string> attributes { get; set; }

        public bool? isLocked { get; set; }
        public int? offset { get; set; }

        public int? limit { get; set; }

        public string search { get; set; }

        public string sortField { get; set; }

        public string sortOrder { get; set; }
    }

    public class SPGetItemsResult : SpecterApiResultBase<SPGetItemResponseData>
    {
        public List<SpecterItem> Items;
        public int TotalCount;
        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterItem>();
            foreach (var itemData in Response.data.items)
            {
                Items.Add(new SpecterItem(itemData));
            }
            TotalCount = Response.data.totalCount;
        }
    }


    public partial class SPAppApiClient
    {
        public async Task<SPGetItemsResult> GetItemsAsync(SPGetItemsRequest request)
        {
            var result = await PostAsync<SPGetItemsResult, SPGetItemResponseData>("/v1/client/app/get-items", AuthType, request);
            return result;
        }
    }

}
