using System;
using System.Collections.Generic;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Stores
{
    public interface IStorePurchaseEntityInfo
    {
        public string id { get; set; }
        public int amount { get; set; }
        public string storeId { get; set; }
        public string collectionId { get; set; }
        public string stackId { get; set; }
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Base class for store purchase item information.
    /// </summary>
    [Serializable]
    public class SPItemPurchaseBaseInfoV2 : IStorePurchaseEntityInfo
    {
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the item to be purchased.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Store ID where the item is available.
        /// </summary>
        public string storeId { get; set; }
        
        /// <summary>
        /// ID of the collection if the item is part of a collection.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// ID of the stack associated with the item.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the item.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Custom purchase item information with price details.
    /// </summary>
    [Serializable]
    public class SPCustomPurchaseItemInfoV2 : SPItemPurchaseBaseInfoV2
    {
        /// <summary>
        /// Custom price per unit for the item.
        /// </summary>
        public double price { get; set; }
        
        /// <summary>
        /// Currency ID for the item's transaction.
        /// </summary>
        public string currencyId { get; set; }
        
        /// <summary>
        /// Real-world currency identifier for the item, if applicable.
        /// </summary>
        public string realWorldCurrencyId { get; set; }
    }
    
    /// <summary>
    /// Base class for store purchase bundle information.
    /// </summary>
    [Serializable]
    public class SPBundlePurchaseBaseInfoV2 : IStorePurchaseEntityInfo
    {
        /// <summary>
        /// Unique identifier for the bundle.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the bundle to be purchased.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Store ID where the bundle is available.
        /// </summary>
        public string storeId { get; set; }
        
        /// <summary>
        /// ID of the collection if the bundle is part of a collection.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// ID of the stack associated with the bundle.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Any additional custom parameters associated with the bundle.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    /// <summary>
    /// Custom purchase bundle information with price details.
    /// </summary>
    [Serializable]
    public class SPCustomPurchaseBundleInfoV2 : SPBundlePurchaseBaseInfoV2
    {
        /// <summary>
        /// Custom price per unit for the bundle.
        /// </summary>
        public double price { get; set; }
        
        /// <summary>
        /// Currency ID for the bundle's transaction.
        /// </summary>
        public string currencyId { get; set; }
        
        /// <summary>
        /// Real-world currency identifier for the bundle, if applicable.
        /// </summary>
        public string realWorldCurrencyId { get; set; }
    }
    
    public partial class SPStoresApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPStoresApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}