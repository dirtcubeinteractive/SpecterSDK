using System;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.Shared
{
    /// <summary>
    /// Represents the sort order options for inventory items.
    /// </summary>
    [Serializable]
    public sealed class SPSortOrder : SPEnum<SPSortOrder>
    {
        public static readonly SPSortOrder Ascending = new SPSortOrder(0, "asc", "Ascending");
        public static readonly SPSortOrder Descending = new SPSortOrder(1, "desc", "Descending");
        
        private SPSortOrder(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public sealed class SPOperations : SPEnum<SPOperations>
    {
        public static readonly SPOperations AddOperation = new SPOperations(0, "add", nameof(AddOperation));
        public static readonly SPOperations SubtractOperation = new SPOperations(1, "subtract", nameof(SubtractOperation));

        private SPOperations(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #region Events and Params

    [Serializable]
    public sealed class SPParamIncrementalType : SPEnum<SPParamIncrementalType>
    {
        public static readonly SPParamIncrementalType OneShot = new SPParamIncrementalType(0, "one-shot", nameof(OneShot));
        public static readonly SPParamIncrementalType Cumulative = new SPParamIncrementalType(1, "cumulative", nameof(Cumulative));
        private SPParamIncrementalType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPParamOperatorType : SPEnum<SPParamOperatorType>
    {
        public static readonly SPParamOperatorType Equal = new SPParamOperatorType(0, "equal", nameof(Equal));
        public static readonly SPParamOperatorType GreaterThanInclusive = new SPParamOperatorType(1, "greaterThanInclusive", nameof(GreaterThanInclusive));
        public static readonly SPParamOperatorType LessThanInclusive = new SPParamOperatorType(2, "lessThanInclusive", nameof(LessThanInclusive));
        public static readonly SPParamOperatorType LessThan = new SPParamOperatorType(3, "lessThan", nameof(LessThan));
        public static readonly SPParamOperatorType GreaterThan = new SPParamOperatorType(4, "greaterThan", nameof(GreaterThan));
        private SPParamOperatorType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPParamType : SPEnum<SPParamType>
    {
        public static readonly SPParamType Statistic = new SPParamType(0, "statistic", nameof(Statistic));
        public static readonly SPParamType State = new SPParamType(1, "state", nameof(State));
        private SPParamType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion


    #region Rewards

    public enum SPRewardType
    {
        ProgressionMarker,
        Currency,
        Item,
        Bundle
    }
    
    [Serializable]
    public sealed class SPRewardSourceType : SPEnum<SPRewardSourceType>
    {
        public static readonly SPRewardSourceType Task = new SPRewardSourceType(0, nameof(Task).ToLower(), nameof(Task));
        public static readonly SPRewardSourceType TaskGroup = new SPRewardSourceType(1, "task_group", nameof(TaskGroup));
        public static readonly SPRewardSourceType Level = new SPRewardSourceType(2, "level", nameof(Level));
        public static readonly SPRewardSourceType Tournament = new SPRewardSourceType(3, nameof(Tournament).ToLower(), nameof(Tournament));
        public static readonly SPRewardSourceType InstantBattle = new SPRewardSourceType(4, "instant_battle", nameof(InstantBattle));
        public static readonly SPRewardSourceType Leaderboard = new SPRewardSourceType(5, nameof(Leaderboard).ToLower(), nameof(Leaderboard));
        public static readonly SPRewardSourceType Custom = new SPRewardSourceType(6, nameof(Custom).ToLower(), nameof(Custom));
        
        private SPRewardSourceType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public class SPRewardGrantType : SPEnum<SPRewardGrantType>
    {
        public static readonly SPRewardGrantType Client = new SPRewardGrantType(0, nameof(Client).ToLower(), nameof(Client));
      
        public static readonly SPRewardGrantType Server = new SPRewardGrantType(1, nameof(Server).ToLower(), nameof(Server));

        public SPRewardGrantType(int id, string name, string displayName = null) : base(id, name, displayName) { }

    }
    
    [Serializable]
    public sealed class SPRewardClaimStatus : SPEnum<SPRewardClaimStatus>
    {
        public static readonly SPRewardClaimStatus Pending = new SPRewardClaimStatus(0, nameof(Pending).ToLower(), nameof(Pending));
        public static readonly SPRewardClaimStatus Completed = new SPRewardClaimStatus(1, nameof(Completed).ToLower(), nameof(Completed));

        private SPRewardClaimStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion
    
    #region Tasks
    
    [Serializable]
    public sealed class SPTaskGroupType : SPEnum<SPTaskGroupType>
    {
        public static readonly SPTaskGroupType Mission = new SPTaskGroupType(0, nameof(Mission).ToLower(), nameof(Mission));
        public static readonly SPTaskGroupType StepSeries = new SPTaskGroupType(1,"step_series", nameof(StepSeries));

        private SPTaskGroupType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPTaskStatus : SPEnum<SPTaskStatus>
    {
        public static readonly SPTaskStatus Pending = new SPTaskStatus(0, nameof(Pending).ToLower(), nameof(Pending));
        public static readonly SPTaskStatus Completed = new SPTaskStatus(1, nameof(Completed).ToLower(), nameof(Completed));
        public static readonly SPTaskStatus RewardClaimed = new SPTaskStatus(2, "reward_claimed", nameof(RewardClaimed));

        private SPTaskStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPTaskGroupStatus : SPEnum<SPTaskGroupStatus>
    {
        public static readonly SPTaskGroupStatus Pending = new SPTaskGroupStatus(0,nameof(Pending).ToLower(), nameof(Pending));
        public static readonly SPTaskGroupStatus Completed = new SPTaskGroupStatus(1, nameof(Completed).ToLower(), nameof(Completed));
        public static readonly SPTaskGroupStatus RewardClaimed = new SPTaskGroupStatus(2, "reward_claimed", nameof(RewardClaimed));

        private SPTaskGroupStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion
    
    #region Economy
    
    [Serializable]
    public sealed class SPCurrencyType : SPEnum<SPCurrencyType>
    {
        public static readonly SPCurrencyType Real = new SPCurrencyType(0, nameof(Real).ToLower(), nameof(Real));
        public static readonly SPCurrencyType Virtual = new SPCurrencyType(1, nameof(Virtual).ToLower(), nameof(Virtual));

        private SPCurrencyType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public class SPPriceTypes : SPEnum<SPPriceTypes>
    {
        public static readonly SPPriceTypes RMG = new SPPriceTypes(0, nameof(RMG), nameof(RMG));
        public static readonly SPPriceTypes VirtualCurrency = new SPPriceTypes(1, "virtual currency", nameof(VirtualCurrency));
        public static readonly SPPriceTypes IAP = new SPPriceTypes(2, nameof(IAP), nameof(IAP));
        
        private SPPriceTypes(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public class SPHostingFeeTypes : SPEnum<SPHostingFeeTypes>
    {
        public static readonly SPHostingFeeTypes Flat = new SPHostingFeeTypes(0, nameof(Flat).ToLower(), nameof(Flat));
        public static readonly SPHostingFeeTypes Percentage = new SPHostingFeeTypes(1, nameof(Percentage).ToLower(), nameof(Percentage));

        private SPHostingFeeTypes(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion

    #region Matches
    
    [Serializable]
    public sealed class SPMatchFormatType : SPEnum<SPMatchFormatType>
    {
        public static readonly SPMatchFormatType SinglePlayer = new SPMatchFormatType(1, "single_player", nameof(SinglePlayer));
        public static readonly SPMatchFormatType MultiPlayer = new SPMatchFormatType(2, "multi_player", nameof(MultiPlayer));
        private SPMatchFormatType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPMatchOutcomeType : SPEnum<SPMatchOutcomeType>
    {
        public static readonly SPMatchOutcomeType Score = new SPMatchOutcomeType(1, "score", nameof(Score));
        public static readonly SPMatchOutcomeType CompletionTime = new SPMatchOutcomeType(2, "completion_time", nameof(CompletionTime));
        public static readonly SPMatchOutcomeType WinLossDraw = new SPMatchOutcomeType(3, "win_loss_draw", nameof(WinLossDraw));
        public static readonly SPMatchOutcomeType FinishPosition = new SPMatchOutcomeType(4, "finish_position", nameof(FinishPosition));
        public static readonly SPMatchOutcomeType WinningsCollected = new SPMatchOutcomeType(5, "winnings_collected", nameof(WinningsCollected));
        private SPMatchOutcomeType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPMatchWinCondition : SPEnum<SPMatchWinCondition>
    {
        public static readonly SPMatchWinCondition Higher = new SPMatchWinCondition(1, nameof(Higher).ToLower(), nameof(Higher));
        public static readonly SPMatchWinCondition Lower = new SPMatchWinCondition(2, nameof(Lower).ToLower(), nameof(Lower));
        
        private SPMatchWinCondition(int id, string name, string displayName) : base(id, name, displayName) { }
    }

    #endregion

    #region Competition

    public sealed class SPCompetitionFormat : SPEnum<SPCompetitionFormat>
    {
        public static readonly SPCompetitionFormat InstantBattle = new SPCompetitionFormat(3, "Instant Battle", nameof(InstantBattle));
        public static readonly SPCompetitionFormat Tournament = new SPCompetitionFormat(2, "Tournament", nameof(Tournament));

        private SPCompetitionFormat(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    #endregion

    #region Leaderboards
    
    [Serializable]
    public sealed class SPLeaderboardOutcomeType : SPEnum<SPLeaderboardOutcomeType>
    {
        public static readonly SPLeaderboardOutcomeType HighScore = new SPLeaderboardOutcomeType(1, "high_score", nameof(HighScore));
        public static readonly SPLeaderboardOutcomeType TimeTrial = new SPLeaderboardOutcomeType(2, "time_trial", nameof(TimeTrial));
        public static readonly SPLeaderboardOutcomeType WinDrawLossPoints = new SPLeaderboardOutcomeType(3, "win_draw_loss_points", nameof(WinDrawLossPoints));
        public static readonly SPLeaderboardOutcomeType PositionWeighting = new SPLeaderboardOutcomeType(4, "position_weighting", nameof(PositionWeighting));
        public static readonly SPLeaderboardOutcomeType CumulativeScore = new SPLeaderboardOutcomeType(5, "cumulative_score", nameof(CumulativeScore));
        private SPLeaderboardOutcomeType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPLeaderboardSourceType : SPEnum<SPLeaderboardSourceType>
    {
        public static readonly SPLeaderboardSourceType Match = new SPLeaderboardSourceType(1, "match", nameof(Match));
        public static readonly SPLeaderboardSourceType Custom = new SPLeaderboardSourceType(3, "custom", nameof(Custom));
        
        private SPLeaderboardSourceType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    #endregion
    
    #region LiveOps
    
    /// <summary>
    /// Values for LiveOps schedule statuses
    /// </summary>
    public sealed class SPScheduleStatus : SPEnum<SPScheduleStatus>
    {
        public static readonly SPScheduleStatus YetToStart = new SPScheduleStatus(0, "yet to start", nameof(YetToStart));
        public static readonly SPScheduleStatus InProgress = new SPScheduleStatus(1, "in progress", nameof(InProgress));
        public static readonly SPScheduleStatus Completed = new SPScheduleStatus(2, "completed", nameof(Completed));
        public static readonly SPScheduleStatus InReview = new SPScheduleStatus(3, "in review", nameof(InReview));
        public static readonly SPScheduleStatus Stopped = new SPScheduleStatus(4, "stopped", nameof(Stopped));
        public static readonly SPScheduleStatus Failed = new SPScheduleStatus(5, "failed", nameof(Failed));

        private SPScheduleStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Values for Schedule statuses only applicable to tasks and task groups.
    /// </summary>
    [Serializable]
    public sealed class SPTasksScheduleStatus : SPEnum<SPTasksScheduleStatus>
    {
        public static readonly SPTasksScheduleStatus YetToStart = new SPTasksScheduleStatus(0, "yet to start", nameof(YetToStart));
        public static readonly SPTasksScheduleStatus InProgress = new SPTasksScheduleStatus(1, "in progress", nameof(InProgress));
        public static readonly SPTasksScheduleStatus Stopped = new SPTasksScheduleStatus(2, "stopped", nameof(Stopped));
        public static readonly SPTasksScheduleStatus Expired = new SPTasksScheduleStatus(3, "expired", nameof(Expired));
        
        private SPTasksScheduleStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    [Serializable]
    public sealed class SPIntervalUnit : SPEnum<SPIntervalUnit>
    {
        public static readonly SPIntervalUnit Days = new SPIntervalUnit(1, nameof(Days).ToLower(), nameof(Days));
        public static readonly SPIntervalUnit Weeks = new SPIntervalUnit(2, nameof(Weeks).ToLower(), nameof(Weeks));
        public static readonly SPIntervalUnit AllTime = new SPIntervalUnit(5, "all_time", nameof(AllTime));
        public static readonly SPIntervalUnit Custom = new SPIntervalUnit(6, nameof(Custom).ToLower(), nameof(Custom));
        public static readonly SPIntervalUnit Minutes = new SPIntervalUnit(7, nameof(Minutes).ToLower(), nameof(Minutes));
        public static readonly SPIntervalUnit Hours = new SPIntervalUnit(8, nameof(Hours).ToLower(), nameof(Hours));
        
        private SPIntervalUnit(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion
}