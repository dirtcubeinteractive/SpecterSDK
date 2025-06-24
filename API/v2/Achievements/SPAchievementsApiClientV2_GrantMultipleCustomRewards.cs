using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Achievements
{
    /// <summary>
    /// Represents a request to grant multiple custom rewards grouped by type.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGrantMultipleCustomRewardsRequest : SPApiRequestBase
    {
        /// <summary>
        /// Object containing rewards grouped by type.
        /// </summary>
        public SPRewardsContainer rewards { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    public class SPGrantMultipleCustomRewardsResult : SpecterApiResultBase<SPGrantMultipleCustomRewardsResponse>
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
        public async Task<SPGrantMultipleCustomRewardsResult> GrantMultipleCustomRewardsAsync(SPGrantMultipleCustomRewardsRequest request)
        {
            var result = await PostAsync<SPGrantMultipleCustomRewardsResult, SPGrantMultipleCustomRewardsResponse>("/v2/client/achievements/grant-reward-batch", AuthType, request);
            return result;
        }
    }
}