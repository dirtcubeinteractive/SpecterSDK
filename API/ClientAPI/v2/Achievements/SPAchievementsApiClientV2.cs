using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Achievements
{
    public partial class SPAchievementsApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPAchievementsApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
    
    /// <summary>
    /// Container for different types of rewards.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardsContainer
    {
        /// <summary>
        /// Array of items to grant.
        /// </summary>
        public List<SPRewardItem> items { get; set; }
        
        /// <summary>
        /// Array of bundles to grant.
        /// </summary>
        public List<SPRewardItem> bundles { get; set; }
        
        /// <summary>
        /// Array of currencies to grant.
        /// </summary>
        public List<SPRewardItem> currencies { get; set; }
        
        /// <summary>
        /// Array of progression markers to grant.
        /// </summary>
        public List<SPRewardItem> progressionMarkers { get; set; }
    }
    
    /// <summary>
    /// Represents a reward item with ID and amount.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardItem
    {
        /// <summary>
        /// Item ID.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the item.
        /// </summary>
        public int amount { get; set; }
    }
}