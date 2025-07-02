using System;
using System.Collections.Generic;
using SpecterSDK.API.v1.Inventory;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v1.Stores
{
    /// <summary>
    /// <para>
    /// The <b>SPStoreApiClient</b> is part of the Specter API, focusing on in-game virtual transactions and store interactions.
    /// This API provides facilities to manage store contents, categories, and handle transactions for a smooth shopping experience for players.
    /// </para>
    /// <para>
    /// The API supports two purchasing methods - 'Default Purchase' for standard transactions using predefined pricing and 
    /// 'Custom Purchase' allowing price override for specific transactions.
    /// </para>
    /// <para>
    /// Integration with this API enriches the player experience and can lead to better player retention and game monetization.
    /// </para>
    /// <remarks>
    /// For more information about configuring stores and store contents for use with the Specter API and SDK, see the
    /// <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/economy/stores">Specter User Manual</a>
    /// </remarks>
    /// </summary>
    public partial class SPStoreApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Store API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPStoreApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
    
    /// <summary>
    /// Represents the data needed to carry out a store purchase.
    /// </summary>
    [Serializable]
    public class SPStorePurchaseData : ISpecterEventConfigurable
    {
        /// <summary>
        /// ID of the resource being purchased (i.e. item ID, bundle ID, etc.)
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// The amount of the resource to purchase. Defaults to 1 if not specified.
        /// </summary>
        public int? amount { get; set; }
        
        /// <summary>
        /// The ID of the currency with which the purchase must be made.
        /// </summary>
        public string currencyId { get; set; }
        
        /// <summary>
        /// The ID of the store from which the purchase must be made.
        /// </summary>
        public string storeId { get; set; }
        
        /// <summary>
        /// The ID of the collection within the player's inventory in which the purchase must
        /// be added. Only applicable when purchasing an item or bundle.
        /// See <see cref="SPInventoryApiClient.SPInventoryEntityInfo"/> for
        /// more info about collection IDs.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Dictionary of optional Specter params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
            
        /// <summary>
        /// Dictionary of optional custom params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}