using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant rewards from a source with custom overrides.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantRewardBySourceWithOverridesRequest : SPApiRequestBase
    {
        /// <summary>
        /// An object representing the origin of the rewards.
        /// </summary>
        public List<SPRewardSourceWithOverrides> sources { get; set; }
    }
    
    /// <summary>
    /// Represents a reward source object with override capabilities.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardSourceWithOverrides
    {
        /// <summary>
        /// The unique identifier for the source.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// The type of source, e.g., task or level.
        /// </summary>
        public SPRewardSourceType type { get; set; }
        
        /// <summary>
        /// Optional unique identifier for the source instance.
        /// </summary>
        public string instanceId { get; set; }
        
        /// <summary>
        /// Flag to bypass lock condition.
        /// </summary>
        public bool? bypassLockCondition { get; set; }
        
        /// <summary>
        /// Flag to bypass limited edition restriction.
        /// </summary>
        public bool? bypassLimitedEdition { get; set; }
        
        /// <summary>
        /// Object containing custom reward configurations.
        /// </summary>
        public SPRewardsContainer overrides { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    public class SPGrantRewardBySourceWithOverridesResult : SpecterApiResultBase<SPGrantRewardBySourceWithOverridesResponse>
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
        public async Task<SPGrantRewardBySourceWithOverridesResult> GrantRewardBySourceWithOverridesAsync(SPGrantRewardBySourceWithOverridesRequest request)
        {
            var result = await PostAsync<SPGrantRewardBySourceWithOverridesResult, SPGrantRewardBySourceWithOverridesResponse>("/v2/client/achievements/grant-reward-by-source-overrides", AuthType, request);
            return result;
        }
    }
}