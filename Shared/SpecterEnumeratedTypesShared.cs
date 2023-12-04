using System;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared
{
    [Serializable]
    public sealed class SPOperations : SPEnum<SPOperations>
    {
        public static readonly SPOperations AddOperation = new SPOperations(0, "add", nameof(AddOperation));
        public static readonly SPOperations SubtractOperation = new SPOperations(1, "subtract", nameof(SubtractOperation));

        private SPOperations(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #region Rewards
    
    [Serializable]
    public sealed class SPRewardSourceType : SPEnum<SPRewardSourceType>
    {
        public static readonly SPRewardSourceType Task = new SPRewardSourceType(0, nameof(Task).ToLower(), nameof(Task));
        public static readonly SPRewardSourceType TaskGroup = new SPRewardSourceType(1, nameof(TaskGroup).ToLower(), nameof(TaskGroup));
        public static readonly SPRewardSourceType LevelUp = new SPRewardSourceType(2, nameof(LevelUp).ToLower(), nameof(LevelUp));
        private SPRewardSourceType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public class SPRewardGrantType : SPEnum<SPRewardGrantType>
    {
        public static readonly SPRewardGrantType Client = new SPRewardGrantType(0, nameof(Client).ToLower(), nameof(Client));
      
        public static readonly SPRewardGrantType Server = new SPRewardGrantType(1, nameof(Server).ToLower(), nameof(Server));

        public SPRewardGrantType(int id, string name, string displayName = null) : base(id, name, displayName) { }

    }
    
    #endregion
    
    #region Economy
    
    [Serializable]
    public class SPPriceTypes : SPEnum<SPPriceTypes>
    {
        public static readonly SPPriceTypes RMG = new SPPriceTypes(0, nameof(RMG), nameof(RMG));
        public static readonly SPPriceTypes VirtualCurrency = new SPPriceTypes(1, "virtual currency", nameof(VirtualCurrency));
        public static readonly SPPriceTypes IAP = new SPPriceTypes(2, nameof(IAP), nameof(IAP));
        
        private SPPriceTypes(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion

    #region Matches
    
    [Serializable]
    public sealed class SPMatchFormatType : SPEnum<SPMatchFormatType>
    {
        public static readonly SPMatchFormatType SinglePlayer = new SPMatchFormatType(1, "single_player", nameof(SinglePlayer));
        public static readonly SPMatchFormatType MultiPlayer = new SPMatchFormatType(2, "multi_player", nameof(MultiPlayer));
        public static readonly SPMatchFormatType MultiPlayerTeam = new SPMatchFormatType(3, "multi_player_team", nameof(MultiPlayerTeam));
        private SPMatchFormatType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPGameMatchOutcomeType : SPEnum<SPGameMatchOutcomeType>
    {
        public static readonly SPGameMatchOutcomeType Score = new SPGameMatchOutcomeType(1, "score", nameof(Score));
        public static readonly SPGameMatchOutcomeType CompletionTime = new SPGameMatchOutcomeType(2, "completion_time", nameof(CompletionTime));
        public static readonly SPGameMatchOutcomeType WinLossDraw = new SPGameMatchOutcomeType(3, "win_loss_draw", nameof(WinLossDraw));
        public static readonly SPGameMatchOutcomeType FinishPosition = new SPGameMatchOutcomeType(4, "finish_position", nameof(FinishPosition));
        public static readonly SPGameMatchOutcomeType WinningsCollected = new SPGameMatchOutcomeType(5, "winnings_collected", nameof(WinningsCollected));
        private SPGameMatchOutcomeType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion
}