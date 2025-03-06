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
    /// Represents the request for executing a custom purchase. 
    /// It provides the ability to override the default item prices for specified transactions.
    /// </summary>
    [Serializable]
    public class SPCustomPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// List of purchase data models with respect to items to be purchased.
        /// </summary>
        public List<SPCustomPurchaseData> items { get; set; }
        
        /// <summary>
        /// List of purchase data models with respect to bundles to be purchased.
        /// </summary>
        public List<SPCustomPurchaseData> bundles { get; set; }
        
        // NOTE: There is no list for currencies because currencies must be configured as
        // part of bundles to be added to a store on the Specter dashboard. When the bundle is purchased,
        // the currency amount will be added to the player's wallet for that currency.
    }

    /// <summary>
    /// Represents the data needed to carry out a custom purchase. Includes an override price property along
    /// with inherited fields from the base purchase class <see cref="SPStorePurchaseData"/>.
    /// </summary>
    [Serializable]
    public class SPCustomPurchaseData : SPStorePurchaseData
    {
        /// <summary>
        /// The override price with which to carry out the purchase.
        /// </summary>
        public double price { get; set; }
    }

    /// <summary>
    /// Represents the result of a custom purchase operation.
    /// </summary>
    public class SPCustomPurchaseResult : SpecterApiResultBase<SPCustomPurchaseResponseData>
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
        /// Asynchronously executes a custom purchase and returns the result.
        /// <remarks>
        /// For full information about the custom purchase endpoint, see the Stores section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// <see cref="SPCustomPurchaseRequest"/> containing details about the custom purchase to be made.
        /// </param>
        /// <returns>
        /// A Task representing the asynchronous operation, with <see cref="SPCustomPurchaseResult"/> as the result type.
        /// </returns>
        public async Task<SPCustomPurchaseResult> CustomPurchaseAsync(SPCustomPurchaseRequest request)
        {
            var result = await PostAsync<SPCustomPurchaseResult, SPCustomPurchaseResponseData>("/v1/client/stores/custom-purchase", AuthType, request);
            return result;
        }
    }
}
