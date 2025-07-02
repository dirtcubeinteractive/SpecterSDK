using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v2.Achievements
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
        public List<SPRewardEntityInfo> items { get; set; }
        
        /// <summary>
        /// Array of bundles to grant.
        /// </summary>
        public List<SPRewardEntityInfo> bundles { get; set; }
        
        /// <summary>
        /// Array of currencies to grant.
        /// </summary>
        public List<SPRewardEntityInfo> currencies { get; set; }
        
        /// <summary>
        /// Array of progression markers to grant.
        /// </summary>
        public List<SPRewardEntityInfo> progressionMarkers { get; set; }
    }
    
    /// <summary>
    /// Represents a reward item with ID and amount.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRewardEntityInfo
    {
        /// <summary>
        /// Item ID.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Quantity of the item.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// Unique id for the collection this reward should be added to. Only applicable to item and bundle rewards.
        /// </summary>
        public string collectionId { get; set; }
        
        /// <summary>
        /// Unique id for the stack this reward should be added to. Only applicable to item and bundle rewards.
        /// </summary>
        public string stackId { get; set; }
        
        /// <summary>
        /// Custom information that may need to stored with the reward.
        /// </summary>
        public Dictionary<string, object> meta { get; set; }
    }
}