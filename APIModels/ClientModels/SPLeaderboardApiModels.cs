using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
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
        private SPLeaderboardSourceType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public sealed class SPLeaderboardInterval : SPEnum<SPLeaderboardInterval>
    {
        public static readonly SPLeaderboardInterval Daily = new SPLeaderboardInterval(1, "daily", nameof(Daily));
        public static readonly SPLeaderboardInterval Weekly = new SPLeaderboardInterval(2, "weekly", nameof(Weekly));
        public static readonly SPLeaderboardInterval Monthly = new SPLeaderboardInterval(3, "monthly", nameof(Monthly));
        public static readonly SPLeaderboardInterval Yearly = new SPLeaderboardInterval(4, "yearly", nameof(Yearly));
        public static readonly SPLeaderboardInterval AllTime = new SPLeaderboardInterval(5, "all_time", nameof(AllTime));
        private SPLeaderboardInterval(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public class SPLeaderboardResponseData : SPResourceResponseData, ISpecterMasterData
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool isRecurring { get; set; }
        public List<SPPrizeDistributionRuleData> prizeDistributionRule { get; set; }
        public int? prizeDistributionOffset { get; set; }       
        public SPMatchResponseBaseData match { get; set; }
        public SPLeaderboardOutcomeData outcomeType { get; set; }
        public SPLeaderboardIntervalData interval { get; set; }
        public SPLeaderboardSourceData sourceType { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        public SPLeaderboardEntryData currentPlayerEntry { get; set; }
        public List<SPLeaderboardEntryData> leaderboardEntries { get; set; }
        public int totalEntries { get; set; }
    }

    [Serializable]
    public class SPUserDetailData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string thumbUrl { get; set; }
    }

    [Serializable]
    public class SPLeaderboardEntryData 
    {
        public int rank { get; set; }
        public int score { get; set; }
        public SPUserDetailData userDetail { get; set; }
    }

    [Serializable]
    public class SPPrizeDistributionRuleData
    {
        public int? startRank { get; set; }
        public int? endRank { get; set; }  
        public List<Dictionary<string, object>> rewardDetails { get; set; }
    }

    [Serializable]
    public class SPLeaderboardOutcomeData
    {
        public int id { get; set; }
        public SPLeaderboardOutcomeType name { get; set; }
    }

    [Serializable]
    public class SPLeaderboardIntervalData 
    {
        public int id { get; set; }
        public SPLeaderboardInterval name { get; set; }
    }

    [Serializable]
    public class SPLeaderboardSourceData 
    {
        public int id { get; set; }
        public SPLeaderboardSourceType name { get; set; }
    }
}