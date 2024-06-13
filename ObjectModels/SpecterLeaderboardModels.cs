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
        public SPCompetitionStatus Status;
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int? IntervalLength;
        public int? Occurrences;
        public bool IsRecurring;
        public SPLeaderboardOutcomeType LeaderboardOutcomeType;
        public SPLeaderboardSourceType Source;
        public SPMatchWinCondition WinCondition;

        protected SpecterEsportsResource(SPESportsResourceResponseData data) : base (data)
        {
            Status = data.status;
            
            LeaderboardOutcomeType = data.outcomeType?.name;
            Source = data.sourceType.name;
            WinCondition = data.winCondition?.name;
            
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
            IsRecurring = data.isRecurring;
        }
    }
    
    public class SpecterLeaderboard : SpecterEsportsResource , ISpecterMasterObject
    {
        public List<SpecterPrizeDistribution> PrizeDistributions;
        public SpecterMatchBase Match;
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterLeaderboard(SPLeaderboardResponseBaseData data) : base(data)
        {
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;

            Status = data.status;
            IsRecurring = data.isRecurring;
            LeaderboardOutcomeType = data.outcomeType.name;

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