using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Stores
{
    [Serializable]
    public class SPDefaultPurchaseRequest : SPApiRequestBase
    {
        public List<SPStorePurchaseData> items { get; set; }
        public List<SPStorePurchaseData> bundles { get; set; }
        public List<SPStorePurchaseData> currencies { get; set; }
    }

    [Serializable]
    public class SPStorePurchaseData
    {
        public string id { get; set; }
        public int? amount { get; set; }
        public string currencyId { get; set; }
        public string storeId { get; set; }
        
        // Id of the collection in which to add the resource within the player's inventory
        public string collectionId { get; set; }
    }
    
    public class SPDefaultPurchaseResult : SpecterApiResultBase<SPDefaultPurchaseResponseData>
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
        public async Task<SPDefaultPurchaseResult> DefaultPurchaseAsync(SPDefaultPurchaseRequest request)
        {
            var result = await PostAsync<SPDefaultPurchaseResult, SPDefaultPurchaseResponseData>("/v1/client/stores/default-purchase", AuthType, request); ;
            return result;
        }
    }

}
