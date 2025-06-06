using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Stores
{
    /// <summary>
    /// Represents a request to process a default purchase with specified items and bundles from a store.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPDefaultPurchaseRequest : SPApiRequestBase
    {
        /// <summary>
        /// Array of items to be purchased.
        /// </summary>
        public List<SPItemPurchaseBaseInfoV2> items { get; set; }
        
        /// <summary>
        /// Array of bundles to be purchased.
        /// </summary>
        public List<SPBundlePurchaseBaseInfoV2> bundles { get; set; }
    }
}