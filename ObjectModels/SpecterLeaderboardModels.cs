using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine.Serialization;

namespace SpecterSDK.ObjectModels
{
    public abstract class SpecterEsportsResource : SpecterResource
    {
        public SPLeaderboardOutcomeType LeaderboardOutcomeType;
        public SPLeaderboardSourceType Source;
        public SPMatchWinCondition WinCondition;

        private SpecterInstanceSchedule Schedule;
        public SPCompetitionStatus Status => Schedule.Status;
        public DateTime InstanceStartDate => Schedule.InstanceStartDate;
        public DateTime? InstanceEndDate => Schedule.InstanceEndDate;
        public SPIntervalUnit IntervalUnit => Schedule.IntervalUnit;
        public int? IntervalLength => Schedule.IntervalLength;
        public int? Occurrences => Schedule.Occurrences;
        public bool IsRecurring => Schedule.IsRecurring;

        protected SpecterEsportsResource(SPESportsResourceResponseData data) : base(data)
        {
            LeaderboardOutcomeType = data.outcomeType?.name;
            Source = data.sourceType?.name;
            WinCondition = data.winCondition?.name;

            Schedule = new SpecterInstanceSchedule()
            {
                Status = data.status,
                InstanceStartDate = data.instanceStartDate,
                InstanceEndDate = data.instanceEndDate,
                IntervalUnit = data.intervalUnit,
                IntervalLength = data.intervalLength,
                Occurrences = data.occurrences ?? 1,
                IsRecurring = data.isRecurring,
            };
        }

        public void SetSchedule(SpecterInstanceSchedule schedule)
        {
            Schedule = schedule;
        }
    }

    public class SpecterLeaderboard : SpecterEsportsResource, ISpecterMasterObject
    {
        public List<SpecterPrizeDistribution> PrizeDistributionRules;
        public SpecterMatchBase Match;
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterLeaderboard(SPLeaderboardResponseBaseData data) : base(data)
        {
            LeaderboardOutcomeType = data.outcomeType.name;

            if (data.match != null)
                Match = new SpecterMatchBase(data.match);

            PrizeDistributionRules = new List<SpecterPrizeDistribution>();
            foreach (var prizeDistributionRule in data.prizeDistributionRules)
            {
                PrizeDistributionRules.Add(new SpecterPrizeDistribution(prizeDistributionRule));
            }

            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SpecterLeaderboardRankings : SpecterEsportsResource
    {
        public string InstanceId;
        public List<SpecterLeaderboardEntry> CurrentPlayerEntries;
        public List<SpecterLeaderboardEntry> LeaderboardEntries;

        public SpecterLeaderboardRankings(SPLeaderboardRankingsResponseData data) : base(data)
        {
            InstanceId = data.instanceId;

            CurrentPlayerEntries = new List<SpecterLeaderboardEntry>();
            if (data.currentPlayerEntries != null)
            {
                foreach (var entry in data.currentPlayerEntries)
                {
                    CurrentPlayerEntries.Add(new SpecterLeaderboardEntry(entry));
                }
            }

            LeaderboardEntries = new List<SpecterLeaderboardEntry>();
            if (data.leaderboardEntries != null)
            {
                foreach (var leaderBoardEntry in data.leaderboardEntries)
                {
                    LeaderboardEntries.Add(new SpecterLeaderboardEntry(leaderBoardEntry));
                }
            }
        }
    }

    public class SpecterLeaderboardEntry
    {
        public int Rank;
        public int Score;
        public string EntryId;
        public SpecterLeaderboardPlayerInfo PlayerInfo;
        public SpecterRewardDetails Prizes;

        public SpecterLeaderboardEntry() { }
        public SpecterLeaderboardEntry(SPLeaderboardEntryData data)
        {
            Rank = data.rank;
            Score = data.score;
            EntryId = data.entryId;
            PlayerInfo = new SpecterLeaderboardPlayerInfo(data.userDetails);

            if (data.prizes != null)
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