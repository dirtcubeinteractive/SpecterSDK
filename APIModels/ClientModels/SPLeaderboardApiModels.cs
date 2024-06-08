using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels
{
    /// <summary>
    /// Master data for leaderboards configured on Specter
    /// </summary>
    [Serializable]
    public class SPLeaderboardResponseBaseData : SPResourceResponseData, ISpecterMasterData
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

    [Serializable]
    public class SPGetLeaderboardsResponseData : ISpecterApiResponseData
    {
        public List<SPLeaderboardResponseBaseData> leaderboards { get; set; }
        public int totalCount { get; set; }
    }
    
    /// <summary>
    /// Data model for leaderboards with their respective rankings.
    /// Also used when a time bound leaderboard's results are fetched, where prizes distributed
    /// by rank are also include. Prizes are null when fetching ONLY rankings.
    /// </summary>
    [Serializable]
    public class SPLeaderboardResponseData : SPLeaderboardResponseBaseData
    {  
        public SPLeaderboardEntryData currentPlayerEntry { get; set; }
        public List<SPLeaderboardEntryData> leaderboardEntries { get; set; }
        public int totalEntries { get; set; }
    }

    [Serializable]
    public class SPLeaderboardEntryData 
    {
        public int rank { get; set; }
        public int score { get; set; }
        public SPLeaderboardPlayerData userDetails { get; set; }
        public SPRewardResourceDetailsResponseData prizes { get; set; }
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
}