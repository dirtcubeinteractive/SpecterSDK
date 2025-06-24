using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
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

    public class SPUpdateWalletResult : SpecterApiResultBase<SPUpdateWalletResponse>
    {
        public SPWalletCurrency UpdatedCurrency { get; set; }
        public long Requested { get; set; }
        public long Applied { get; set; }
        public bool WasAdjusted { get; set; }
        public string AdjustmentReason { get; set; }

        public long UpdatedBalance => UpdatedCurrency.Balance;
        
        protected override void InitSpecterObjectsInternal()
        {
            UpdatedCurrency = new SPWalletCurrency(Response.data);
            
            Applied = Response.data.applied;
            Requested = Response.data.requested;
            WasAdjusted = Response.data.wasAdjusted;
            AdjustmentReason = Response.data.adjustmentReason;
        }
    }

    public partial class SPWalletApiClientV2
    {
        public async Task<SPUpdateWalletResult> UpdateWalletAsync(SPUpdateWalletRequest request)
        {
            var result = await PostAsync<SPUpdateWalletResult, SPUpdateWalletResponse>("/v2/client/wallet/update-balance", AuthType, request);
            return result;
        }
    }
}