using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPLeaderboardResponseData : SPResourceResponseData, ISpecterMasterData
    {
        public DateTime instanceStartDate { get; set; }
        public DateTime? instanceEndDate { get; set; }
        public SPIntervalUnit intervalUnit { get; set; }
        public int intervalLength { get; set; }
        public int? occurrences { get; set; }
        public SPCompetitionStatus status { get; set; }
        public bool isRecurring { get; set; }
        public List<SPPrizeDistributionData> prizeDistributionRules { get; set; }
        public SPMatchResponseBaseData match { get; set; }
        public SPLeaderboardOutcomeData outcomeType { get; set; }
        public SPLeaderboardSourceData sourceType { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
        public SPLeaderboardEntryData currentPlayerEntry { get; set; }
        public List<SPLeaderboardEntryData> leaderboardEntries { get; set; }
        public int totalEntries { get; set; }
        public DateTime? resetTime { get; set; }
    }

    [Serializable]
    public class SPLeaderboardPlayerData
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
        public SPLeaderboardPlayerData userDetails { get; set; }
        private SPRewardResourceDetailsResponseData prizes { get; set; }
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
        public SPIntervalUnit name { get; set; }
    }

    [Serializable]
    public class SPLeaderboardSourceData 
    {
        public int id { get; set; }
        public SPLeaderboardSourceType name { get; set; }
    }
}