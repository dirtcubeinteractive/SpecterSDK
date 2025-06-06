using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Stores
{
    /// <summary>
    /// Represents a request to process a custom purchase with specified items and bundles, allowing overrides to configured pricing.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCustomPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items with custom pricing to be purchased.
        /// </summary>
        public List<SPCustomPurchaseItemInfoV2> items { get; set; }
        
        /// <summary>
        /// Array of bundles with custom pricing to be purchased.
        /// </summary>
        public List<SPCustomPurchaseBundleInfoV2> bundles { get; set; }
    }
}