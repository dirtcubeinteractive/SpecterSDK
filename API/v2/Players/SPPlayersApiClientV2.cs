using System;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.API.v2.Players.Others;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Players
{
    public class SPPlayersApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPOtherPlayerClientV2 Other { get; private set; }
        public SPMePlayerClientV2 Me { get; private set; }
        
        public SPPlayersApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
            Other = new SPOtherPlayerClientV2(config);
            Me = new SPMePlayerClientV2(config);
        }
    }
    
    [Serializable]
    public class SPRewardHistoryAttribute : SPEnum<SPRewardHistoryAttribute>
    {
        public static readonly SPRewardHistoryAttribute RewardDetails = new SPRewardHistoryAttribute(0, "rewardDetails", "Reward Details");
        
        public SPRewardHistoryAttribute(int id, string name, string displayName = null) : base(id, name, displayName)
        {
        }
    }
    
    /// <summary>
    /// Represents the attributes available for the Task Group Status endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTaskGroupStatusAttribute : SPEnum<SPTaskGroupStatusAttribute>
    {
        public static readonly SPTaskGroupStatusAttribute Tasks = new SPTaskGroupStatusAttribute(0, "tasks", "Tasks");
        
        private SPTaskGroupStatusAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a player data key-value pair.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPlayerDataKeyValue
    {
        /// <summary>
        /// Unique key for the data field.
        /// </summary>
        public string key { get; set; }
        
        /// <summary>
        /// The value associated with the key.
        /// </summary>
        public object value { get; set; }
    }
}