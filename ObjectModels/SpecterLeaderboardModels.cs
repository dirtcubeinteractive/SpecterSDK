using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterLeaderboardBase : SpecterResource , ISpecterMasterObject
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
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterLeaderboardBase(SPLeaderboardResponseBaseData data) : base(data)
        {
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;

            Status = data.status;
            IsRecurring = data.isRecurring;
            LeaderboardOutcomeType = data.outcomeType.name;
            LeaderboardSourceType = data.sourceType.name;

            if (data.match != null)
                Match = new SpecterMatchBase(data.match);

            PrizeDistributions = new List<SpecterPrizeDistribution>();
            foreach (var prizeDistributionRule in data.prizeDistributionRules)
            {
                PrizeDistributions.Add(new SpecterPrizeDistribution(prizeDistributionRule));
            }

            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SpecterLeaderboard : SpecterLeaderboardBase
    {
        public SpecterLeaderboardEntry CurrentPlayerEntry;
        public List<SpecterLeaderboardEntry> LeaderboardEntries;
        public SpecterLeaderboard(SPLeaderboardResponseData data) : base(data)
        {
            if (data.currentPlayerEntry != null)
                CurrentPlayerEntry = new SpecterLeaderboardEntry(data.currentPlayerEntry);
             
            LeaderboardEntries = new List<SpecterLeaderboardEntry>();
            foreach (var leaderBoardEntry in data.leaderboardEntries)
            {
                LeaderboardEntries.Add(new SpecterLeaderboardEntry(leaderBoardEntry));
            }
        }
    }

    public class SpecterLeaderboardEntry
    {
        public int Rank;
        public int Score;
        public SpecterLeaderboardPlayerInfo PlayerInfo;
        public SpecterRewardDetails Prizes;

        public SpecterLeaderboardEntry() { }
        public SpecterLeaderboardEntry(SPLeaderboardEntryData data)
        {
            Rank = data.rank;
            Score = data.score;
            PlayerInfo = new SpecterLeaderboardPlayerInfo(data.userDetails);

            if(data.prizes != null)
            Prizes = new SpecterRewardDetails(data.prizes);
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