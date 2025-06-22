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
    /// Represents the attributes available for the Match History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPMatchHistoryAttribute : SPEnum<SPMatchHistoryAttribute>
    {
        public static readonly SPMatchHistoryAttribute Competition = new SPMatchHistoryAttribute(0, "competition", "Competition");
        public static readonly SPMatchHistoryAttribute PlayerDetails = new SPMatchHistoryAttribute(1, "playerDetails", "Player Details");
        
        private SPMatchHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents the attributes available for the Instant Battle History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPInstantBattleHistoryAttribute : SPEnum<SPInstantBattleHistoryAttribute>
    {
        public static readonly SPInstantBattleHistoryAttribute Match = new SPInstantBattleHistoryAttribute(0, "match", "Match");
        public static readonly SPInstantBattleHistoryAttribute Config = new SPInstantBattleHistoryAttribute(1, "config", "Configuration");
        public static readonly SPInstantBattleHistoryAttribute Type = new SPInstantBattleHistoryAttribute(2, "type", "Type");
        public static readonly SPInstantBattleHistoryAttribute SourceType = new SPInstantBattleHistoryAttribute(3, "sourceType", "Source Type");
        
        private SPInstantBattleHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    // TODO: Verify if rankingMethod should be an attribute for Tourney History
    /// <summary>
    /// Represents the attributes available for the Tournament History endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPTournamentHistoryAttribute : SPEnum<SPTournamentHistoryAttribute>
    {
        public static readonly SPTournamentHistoryAttribute Match = new SPTournamentHistoryAttribute(0, nameof(Match).ToLower(), nameof(Match));
        public static readonly SPTournamentHistoryAttribute SourceType = new SPTournamentHistoryAttribute(1, "sourceType", "Source Type");
        public static readonly SPTournamentHistoryAttribute Config = new SPTournamentHistoryAttribute(2, nameof(Config).ToLower(), nameof(Config));
        public static readonly SPTournamentHistoryAttribute Type = new SPTournamentHistoryAttribute(4, "type", "Type");
        
        private SPTournamentHistoryAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
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