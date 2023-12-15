using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterLeaderboard : SpecterResource, ISpecterMasterObject
    {
        public DateTime? StartDate;
        public DateTime? EndDate;
        public bool IsRecurring;
        public List<SpecterPrizeDistributionRule> PrizeDistributionRules;
        public SpecterMatchBase Match;
        public SPLeaderboardOutcomeType LeaderboardOutcomeType;
        public SPLeaderboardSourceType LeaderboardSourceType;
        public SPLeaderboardInterval LeaderboardInterval;
        public int TotalCount;
        public SpecterLeaderboardEntry CurrentPlayerEntry;
        public List<SpecterLeaderboardEntry> LeaderboardEntries;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterLeaderboard(SPLeaderboardResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            StartDate = data.startDate;
            EndDate = data.endDate;
            IsRecurring = data.isRecurring;
            LeaderboardOutcomeType = data.outcomeType.name;
            LeaderboardSourceType = data.sourceType.name;
            LeaderboardInterval = data.interval.name;
            TotalCount = data.totalEntries;
            
            Match = new SpecterMatchBase(data.match);
            CurrentPlayerEntry = new SpecterLeaderboardEntry(data.currentPlayerEntry);
            
            PrizeDistributionRules = new List<SpecterPrizeDistributionRule>();
            foreach (var prizeDistributionRule in PrizeDistributionRules)
            {
                PrizeDistributionRules.Add(prizeDistributionRule);
            }
            
            LeaderboardEntries = new List<SpecterLeaderboardEntry>();
            foreach (var leaderBoardEntry in data.leaderboardEntries)
            {
                LeaderboardEntries.Add(new SpecterLeaderboardEntry(leaderBoardEntry));
            }
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterPrizeDistributionRule
    {
        public readonly int StartRank;
        public readonly int EndRank;
        public List<Dictionary<string, object>> RewardDetails;

        public SpecterPrizeDistributionRule(SPPrizeDistributionRuleData data)
        {
            StartRank = data.startRank ?? 1;
            EndRank = data.endRank ?? StartRank;
            RewardDetails = data.rewardDetails;
        }
    }

    public class SpecterLeaderboardEntry
    {
        public int Rank;
        public int Score;
        public SpecterLeaderboardPlayerInfo PlayerInfo;
        
        public SpecterLeaderboardEntry(SPLeaderboardEntryData data)
        {
            Rank = data.rank;
            Score = data.score;
            PlayerInfo = new SpecterLeaderboardPlayerInfo(data.userDetails);
        }
    }

    public class SpecterLeaderboardPlayerInfo
    {
        public string Uuid;
        public string Id;
        public string FirstName;
        public string LastName;
        public string Username;
        public string DisplayName;
        public string ThumbUrl;
        
        public SpecterLeaderboardPlayerInfo(SPLeaderboardPlayerData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            FirstName = data.firstName;
            LastName = data.lastName;
            Username = data.username;
            DisplayName = data.displayName;
            ThumbUrl = data.thumbUrl;
        }
    }
}