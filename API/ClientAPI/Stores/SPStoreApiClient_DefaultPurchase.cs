using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.Stores
{
    /// <summary>
    /// Represents the request for executing a default purchase with preconfigured
    /// pricing.
    /// </summary>
    [Serializable]
    public class SPDefaultPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// List of purchase data models with respect to items to be purchased.
        /// </summary>
        public List<SPStorePurchaseData> items { get; set; }
        
        /// <summary>
        /// List of purchase data models with respect to bundles to be purchased.
        /// </summary>
        public List<SPStorePurchaseData> bundles { get; set; }
        
        // NOTE: There is no list for currencies because currencies must be configured as
        // part of bundles to be added to a store on the Specter dashboard. When the bundle is purchased,
        // the currency amount will be added to the player's wallet for that currency.
    }
    
    /// <summary>
    /// Represents the result of a default purchase operation.
    /// </summary>
    public class SPDefaultPurchaseResult : SpecterApiResultBase<SPDefaultPurchaseResponseData>
    {
        // List of inventory item instances created or updated on purchase completion.
        public List<SpecterInventoryItem> Items;
        
        // List of inventory bundle instances created or updated on purchase completion.
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
        /// <summary>
        /// Asynchronously executes a default purchase and returns the result.
        /// <remarks>
        /// For full information about the default purchase endpoint, see the Stores section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// <see cref="SPDefaultPurchaseRequest"/> containing details about the default purchase to be made.
        /// </param>
        /// <returns>
        /// A Task representing the asynchronous operation, with <see cref="SPDefaultPurchaseResult"/> as the result type.
        /// </returns>
        public async Task<SPDefaultPurchaseResult> DefaultPurchaseAsync(SPDefaultPurchaseRequest request)
        {
            var result = await PostAsync<SPDefaultPurchaseResult, SPDefaultPurchaseResponseData>("/v1/client/stores/default-purchase", AuthType, request); ;
            return result;
        }
    }

}
