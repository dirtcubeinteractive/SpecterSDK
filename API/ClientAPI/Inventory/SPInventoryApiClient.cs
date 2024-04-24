using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Inventory
{
    /// <summary>
    /// <para>
    /// The SPInventoryApiClient class provides methods for managing in-game assets like items and bundles. 
    /// This is part of Specter's Inventory API, which equips developers with endpoints to add, retrieve,
    /// equip, consume, and remove assets from a player's inventory.
    /// </para>
    /// <para>
    /// The enhancement of in-game experiences through seamless interaction with in-game assets is one of
    /// the key benefits of using the Inventory API.
    /// </para>
    /// <para>
    /// See the Inventory section in the <a href="https://doc.specterapp.xyz">Specter API Documentation</a> for more info.
    /// </para>
    /// </summary>
    public partial class SPInventoryApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// This class is an object used to represent information about a bundle or item instance in Inventory API requests.
        /// </summary>
        [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class SPInventoryEntityInfo : ISpecterEventConfigurable
        {
            /// <summary>
            /// The ID of the item or bundle as configured by you on the dashboard
            /// </summary>
            public string id;
            
            /// <summary>
            /// The instance ID of the asset.
            /// An instance ID is generated when an asset is added to the inventory, and is only relevant in inventory
            /// calls like Add, Consume, Remove.
            /// </summary>
            public string instanceId;
            
            /// <summary>
            /// The number of instances of the asset to be updated in the inventory.
            /// </summary>
            public int? amount;
            
            /// <summary>
            /// A collection ID which you define in your code. The collection concept is simply to allow
            /// developers to group inventory items according to the needs of the game.
            /// </summary>
            /// <example>
            /// If your game allows players to buy several kinds of vehicles, you may have collection IDs like <b>bikes_collection</b>, <b>cars_collection</b>, <b>trucks_collection</b>, etc.
            /// </example>
            public string collectionId;
            
            /// <summary>
            /// Dictionary of optional Specter params to be sent with the API call
            /// </summary>
            public Dictionary<string, object> specterParams { get; set; }
            
            /// <summary>
            /// Dictionary of optional custom params to be sent with the API call
            /// </summary>
            public Dictionary<string, object> customParams { get; set; }
        }
        
        /// <summary>
        /// The Inventory API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        public SPInventoryApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}