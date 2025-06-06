using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Wallet
{
    /// <summary>
    /// Represents a request to update a wallet balance.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateWalletRequest : SPApiRequestBase
    {
        /// <summary>
        /// Currency identifier for which the balance will be updated.
        /// </summary>
        public string currencyId { get; set; }
        
        /// <summary>
        /// Operation to perform on the wallet balance, either 'add' or 'subtract'.
        /// </summary>
        public SPOperations operation { get; set; }
        
        /// <summary>
        /// The amount to add or subtract from the wallet balance.
        /// </summary>
        public long amount { get; set; }
        
        /// <summary>
        /// The type of the wallet to update (mainly relevant when updating RMG wallets. Default to virtual).
        /// </summary>
        public SPWalletType walletType { get; set; } = SPWalletType.Virtual;
        
        /// <summary>
        /// Custom parameters for additional processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}