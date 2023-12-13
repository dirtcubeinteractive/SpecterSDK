using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Inventory
{

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserInventoryRequest : SPApiRequestBase
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

    public class SPGetUserInventoryResult : SpecterApiResultBase<SPGetUserInventoryResponseData>
    {
        public List<SpecterInventoryItem> Items;
        public List<SpecterInventoryBundle> Bundles;

        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterInventoryItem>();
            foreach (var itemData in Response.data.items)
                Items.Add(new SpecterInventoryItem(itemData));

            Bundles = new List<SpecterInventoryBundle>();
            foreach (var bundleData in Response.data.bundles)
                Bundles.Add(new SpecterInventoryBundle(bundleData));
        }
    }

    public partial class SPInventoryApiClient
    {
        public async Task<SPGetUserInventoryResult> GetItemsFromInventory(SPGetUserInventoryRequest request)
        {
            var result = await PostAsync<SPGetUserInventoryResult, SPGetUserInventoryResponseData>("/v1/client/inventory/get-inventory", AuthType, request);
            return result;
        }
    }
}
