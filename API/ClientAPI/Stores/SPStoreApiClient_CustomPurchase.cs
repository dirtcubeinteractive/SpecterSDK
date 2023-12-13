using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Stores
{
    [Serializable]
    public class SPCustomPurchaseRequest : SPApiRequestBase
    {
        public List<SPCustomPurchaseData> items { get; set; }
        public List<SPCustomPurchaseData> bundles { get; set; }
        public List<SPCustomPurchaseData> currencies { get; set; }
    }

    [Serializable]
    public class SPCustomPurchaseData : SPStorePurchaseData
    {
        public double price { get; set; }
    }

    public class SPCustomPurchaseResult : SpecterApiResultBase<SPCustomPurchaseResponseData>
    {
        public List<SpecterInventoryItem> Items;
        public List<SpecterInventoryBundle> Bundles;
        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterInventoryItem>();
            foreach (var item in Response.data.items)
                Items.Add(new SpecterInventoryItem(item));
            
            Bundles = new List<SpecterInventoryBundle>();
            foreach (var bundle in Response.data.bundles)
                Bundles.Add(new SpecterInventoryBundle(bundle));
        }
    }

    public partial class SPStoreApiClient
    {
        public async Task<SPCustomPurchaseResult> CustomPurchaseAsync(SPCustomPurchaseRequest request)
        {
            var result = await PostAsync<SPCustomPurchaseResult, SPCustomPurchaseResponseData>("/v1/client/stores/custom-purchase", AuthType, request);
            return result;
        }
    }
}
