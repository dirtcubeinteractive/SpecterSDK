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

    [Serializable]
    public sealed class SPScheduleStates : SPEnum<SPScheduleStates>
    {
        public static readonly SPScheduleStates YetToStart = new SPScheduleStates(0, "yet to start", nameof(YetToStart));
        public static readonly SPScheduleStates InProgress = new SPScheduleStates(1, "in progress", nameof(InProgress));
        public static readonly SPScheduleStates Stopped = new SPScheduleStates(2, "stopped", nameof(Stopped));
        public static readonly SPScheduleStates Expired = new SPScheduleStates(3, "expired", nameof(Expired));
        
        private SPScheduleStates(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
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
        public static readonly SPRewardSourceType Competition = new SPRewardSourceType(3, nameof(Competition).ToLower(), nameof(Competition));
        public static readonly SPRewardSourceType Leaderboard = new SPRewardSourceType(4, nameof(Leaderboard).ToLower(), nameof(Leaderboard));
        public static readonly SPRewardSourceType Custom = new SPRewardSourceType(5, nameof(Custom).ToLower(), nameof(Custom));
        
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
    public sealed class SPTaskType : SPEnum<SPTaskType>
    {
        public static readonly SPTaskType Static = new SPTaskType(0, nameof(Static).ToLower(), nameof(Static));
        public static readonly SPTaskType Daily = new SPTaskType(1, nameof(Daily).ToLower(), nameof(Daily));
        public static readonly SPTaskType Weekly = new SPTaskType(2, nameof(Weekly).ToLower(), nameof(Weekly));
        
        private SPTaskType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
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

    #region Competition


    public sealed class SPCompetitionStatus : SPEnum<SPCompetitionStatus>
    {
        public static readonly SPCompetitionStatus YetToStart = new SPCompetitionStatus(0, "yet to start", nameof(YetToStart));
        public static readonly SPCompetitionStatus InProgress = new SPCompetitionStatus(1, "in progress", nameof(InProgress));
        public static readonly SPCompetitionStatus Completed = new SPCompetitionStatus(2, "completed", nameof(Completed));
        public static readonly SPCompetitionStatus InReview = new SPCompetitionStatus(3, "in review", nameof(InReview));
        public static readonly SPCompetitionStatus Stopped = new SPCompetitionStatus(4, "stopped", nameof(Stopped));
        public static readonly SPCompetitionStatus Failed = new SPCompetitionStatus(5, "failed", nameof(Failed));

        public SPCompetitionStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    public sealed class SPCompetitionFormat : SPEnum<SPCompetitionFormat>
    {
        public static readonly SPCompetitionFormat InstantBattle = new SPCompetitionFormat(1, "Instant Battle", nameof(InstantBattle));
        public static readonly SPCompetitionFormat PaidChallenge = new SPCompetitionFormat(1, "Paid Challenge", nameof(PaidChallenge));
        public static readonly SPCompetitionFormat Bracket = new SPCompetitionFormat(1, "Bracket", nameof(Bracket));
        public static readonly SPCompetitionFormat Tournament = new SPCompetitionFormat(1, "Tournament", nameof(Tournament));

        public SPCompetitionFormat(int id, string name, string displayName = null) : base(id, name, displayName) { }

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
        public static readonly SPLeaderboardSourceType Statistics = new SPLeaderboardSourceType(2, "statistics", nameof(Statistics));
        public static readonly SPLeaderboardSourceType Custom = new SPLeaderboardSourceType(3, "custom", nameof(Custom));
        
        private SPLeaderboardSourceType(int id, string name, string displayName = null) : base(id, name, displayName) { }
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