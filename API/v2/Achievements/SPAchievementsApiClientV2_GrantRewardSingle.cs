using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant a single reward.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardSingleRequest : SPApiRequestBase
    {
        /// <summary>
        /// Type of the reward (e.g., item, bundle).
        /// </summary>
        public SPRewardType type { get; set; }
        
        /// <summary>
        /// Unique identifier of the reward.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the reward to grant.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Flag to bypass lock condition.
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to bypass limited edition restriction.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    public class SPGrantRewardSingleResult : SpecterApiResultBase<SPGrantRewardSingleResponse>
    {
        public List<SPInventoryItem> Items { get; set; }
        public List<SPInventoryBundle> Bundles { get; set; }
        public List<SPWalletCurrency> Currencies { get; set; }
        public List<SPMarkerProgress> ProgressionMarkers { get; set; }
        
        public List<SPFailedRewards> FailedRewards { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Items = Response.data?.items?.ConvertAll(x => new SPInventoryItem(x)) ?? new List<SPInventoryItem>();
            Bundles = Response.data?.bundles?.ConvertAll(x => new SPInventoryBundle(x)) ?? new List<SPInventoryBundle>();
            Currencies = Response.data?.currencies?.ConvertAll(x => new SPWalletCurrency(x)) ?? new List<SPWalletCurrency>();
            ProgressionMarkers = Response.data?.progressionMarkers?.ConvertAll(x => new SPMarkerProgress(x)) ?? new List<SPMarkerProgress>();
            
            FailedRewards = Response.data?.failedRewards?.ConvertAll(x => new SPFailedRewards(x)) ?? new List<SPFailedRewards>();
        }
    }

    public partial class SPAchievementsApiClientV2
    {
        public async Task<SPGrantRewardSingleResult> GrantRewardSingleAsync(SPGrantRewardSingleRequest request)
        {
            var result = await PostAsync<SPGrantRewardSingleResult, SPGrantRewardSingleResponse>("/v2/client/achievements/grant-reward-single", AuthType, request);
            return result;
        }
    }
}