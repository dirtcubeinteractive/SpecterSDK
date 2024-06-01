using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterLeaderboard : SpecterResource, ISpecterMasterObject
    {
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int IntervalLength;
        public int? Occurrences;
        
        public SPCompetitionStatus Status;
        public bool IsRecurring;
        public List<SpecterPrizeDistribution> PrizeDistributions;
        public SpecterMatchBase Match;
        public SPLeaderboardOutcomeType LeaderboardOutcomeType;
        public SPLeaderboardSourceType LeaderboardSourceType;
        public int TotalCount;
        public SpecterLeaderboardEntry CurrentPlayerEntry;
        public List<SpecterLeaderboardEntry> LeaderboardEntries;
        public DateTime? ResetTime { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SpecterLeaderboard(SPLeaderboardResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
            
            Status = data.status;
            IsRecurring = data.isRecurring;
            LeaderboardOutcomeType = data.outcomeType.name;
            LeaderboardSourceType = data.sourceType.name;
            TotalCount = data.totalEntries;
            ResetTime = data.resetTime;
            
            if (data.match != null)
                Match = new SpecterMatchBase(data.match);
            
            if (data.currentPlayerEntry != null)
                CurrentPlayerEntry = new SpecterLeaderboardEntry(data.currentPlayerEntry);
            
            PrizeDistributions = new List<SpecterPrizeDistribution>();
            foreach (var prizeDistributionRule in data.prizeDistributionRules)
            {
                PrizeDistributions.Add(new SpecterPrizeDistribution(prizeDistributionRule));
            }
            
            LeaderboardEntries = new List<SpecterLeaderboardEntry>();
            foreach (var leaderBoardEntry in data.leaderboardEntries)
            {
                LeaderboardEntries.Add(new SpecterLeaderboardEntry(leaderBoardEntry));
            }
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SpecterLeaderboardEntry
    {
        public int Rank;
        public int Score;
        public SpecterLeaderboardPlayerInfo PlayerInfo;

        public SpecterLeaderboardEntry() { }
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