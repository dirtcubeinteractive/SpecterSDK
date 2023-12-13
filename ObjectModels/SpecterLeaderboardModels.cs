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
        public List<SpecterPrizeDistributionRuleData> PrizeDistributionRules;
        public int? PrizeDistributionOffset;
        public SpecterMatchBase Match;
        public SpecterGameBase Game;
        public SPLeaderboardOutcomeType LeaderboardOutcomeType;
        public SPLeaderboardSourceType LeaderboardSourceType;
        public SPLeaderboardInterval LeaderboardInterval;
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

            PrizeDistributionRules = new List<SpecterPrizeDistributionRuleData>();
            foreach (var prizeDistributionRule in PrizeDistributionRules)
            {
                PrizeDistributionRules.Add(prizeDistributionRule);
            }

            PrizeDistributionOffset = data.prizeDistributionOffset;
            Match = new SpecterMatchBase(data.match);
            Game = new SpecterGameBase(data.game);
            LeaderboardOutcomeType = data.outcomeType.name;
            LeaderboardSourceType = data.sourceType.name;
            LeaderboardInterval = data.interval.name;
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterPrizeDistributionRuleData
    {
        public int? StartRank;
        public int? EndRank;
        public List<Dictionary<string, object>> RewardDetails;

        public SpecterPrizeDistributionRuleData(SPPrizeDistributionRuleData data)
        {
            StartRank = data.startRank;
            EndRank = data.endRank;
            RewardDetails = data.rewardDetails;
        }
    }

    public class SpecterLeaderboardEntryResult 
    {
        public int TotalCount;
        public SpecterLeaderboardEntry CurrentPlayerEntry;
        public List<SpecterLeaderboardEntry> LeaderboardEntries;
        public SpecterLeaderboardEntryResult(SPLeaderboardEntriesResponseData data)
        {
            CurrentPlayerEntry = new SpecterLeaderboardEntry(data.currentPlayer);
            LeaderboardEntries = new List<SpecterLeaderboardEntry>();
            foreach (var leaderBoardDetail in data.leaderboardDetails)
            {
                LeaderboardEntries.Add(new SpecterLeaderboardEntry(leaderBoardDetail));
            }
            TotalCount = data.totalCount;
        }
    }

    public class SpecterLeaderboardEntry
    {
        public int Rank;
        public int Score;
        public SpecterUserDetail UserDetail;
        public SpecterLeaderboardEntry(SPLeaderboardEntryData data)
        {
            Rank = data.rank;
            Score = data.score;
            UserDetail = new SpecterUserDetail(data.userDetail);
        }
    }

    public class SpecterUserDetail
    {
        public string Uuid;
        public string Id;
        public string FirstName;
        public string LastName;
        public string Username;
        public string DisplayName;
        public string ThumbUrl;
        public SpecterUserDetail(SPUserDetailData data)
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