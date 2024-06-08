using System;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK.ObjectModels
{

    public class SpecterCompetitionBase : SpecterResource
    {
        public int MinPlayers;
        public int? MaxPlayers;
        public int? MaxEntryAllowed;
        public int? MaxAttemptAllowed;
        public SPCompetitionFormat Format;
        public SpecterMatchBase MatchData;
        public SpecterGameBase GameData;
        public SPCompetitionStatus Status;
        
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int? IntervalLength;
        public int? Occurrences;

        public SpecterCompetitionBase(SPCompetitionResponseBaseData data) : base(data)
        {
            MinPlayers = data.minPlayers ?? 0;
            MaxPlayers = data.maxPlayers;
            MaxEntryAllowed = data.maxEntryAllowed;
            MaxAttemptAllowed = data.maxAttemptAllowed;
            Status = data.status;
            Format = data.formatType.name;
            MatchData = data.match != null ? new SpecterMatchBase(data.match) : null;
            GameData = data.game != null ? new SpecterGameBase(data.game) : null;
            
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
        }
    }

    public class SpecterCompetition : SpecterCompetitionBase, ISpecterMasterObject
    {
        public List<SpecterPrizeDistribution> PrizeDistributions;
        public List<SpecterUnlockCondition> UnlockConditions;
        public List<SpecterEntryFee> EntryFees;

        public bool HasEntryFee => EntryFees.Count > 0;

        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterCompetition(SPCompetitionResponseData data) : base(data)
        {
            UnlockConditions = new List<SpecterUnlockCondition>();
            if (data.unlockConditions != null)
            {
                foreach (var condition in data.unlockConditions)
                {
                    UnlockConditions.Add(new SpecterUnlockCondition(condition));
                }
            }

            EntryFees = new List<SpecterEntryFee>();
            if (data.entryFees != null)
            {
                foreach (var fee in data.entryFees)
                {
                    EntryFees.Add(new SpecterEntryFee(fee));
                }
            }

            PrizeDistributions = new List<SpecterPrizeDistribution>();
            if (data.prizeDistributionRules != null)
            {
                foreach (var prize in data.prizeDistributionRules)
                {
                    PrizeDistributions.Add(new SpecterPrizeDistribution(prize));
                }
            }
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SpecterEnteredCompetition : SpecterCompetitionBase
    {
        public string InstanceId { get; set; }
        public List<SpecterCompetitionEntryInfo> Entries { get; set; }
        
        public SpecterEnteredCompetition(SPEnteredCompetitionResponseData data) : base(data)
        {
            InstanceId = data.instanceId;
            Entries = new List<SpecterCompetitionEntryInfo>();

            if (data.entries != null)
            {
                foreach (var entry in data.entries)
                {
                    Entries.Add(new SpecterCompetitionEntryInfo()
                    {
                        EntryId = entry.entryId,
                        NumberOfAttemptsLeft = entry.numberOfAttemptsLeft
                    });
                }
            }
        }
    }

    public class SpecterCompetitionEntryInfo
    {
        public string EntryId { get; set; }
        public int? NumberOfAttemptsLeft { get; set; }
    }


    public class SpecterCompetitionResultEntry : SpecterLeaderboardEntry
    {
        public string EntryId; 

        public SpecterCompetitionResultEntry(SPCompetitionLeaderboardEntryData data) : base(data)
        {
            EntryId = data.entryId;
        }
    }

    public class SpecterESportsResult : SpecterResource
    {
        public string InstanceId;
        public SPCompetitionStatus Status;
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int IntervalLength; 
        public int? Occurrences;
        public bool IsRecurring;
        public int TotalEntries;

        public SpecterESportsResult(SPESportsResultResponseData data)
        {
            InstanceId = data.instanceId;
            Status = data.status;
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
            IsRecurring = data.isRecurring;
            TotalEntries = data.totalEntries;
        }
    }

    public class SpecterCompetitionResult : SpecterESportsResult
    {
        public SPCompetitionFormat Format;
        
        public List<SpecterCompetitionResultEntry> CurrentPlayerEntries;
        public List<SpecterCompetitionResultEntry> CompetitionEntries;

        public SpecterCompetitionResult(SPCompetitionResultResponseData data) : base(data)
        {
            Format = data.formatType.name;
            CurrentPlayerEntries = new List<SpecterCompetitionResultEntry>();
            foreach (var currentEntry in data.currentPlayerEntries)
            {
                CurrentPlayerEntries.Add(new SpecterCompetitionResultEntry(currentEntry));
            }

            CompetitionEntries = new List<SpecterCompetitionResultEntry>();
            foreach (var currentEntry in data.competitionEntries)
            {
                CompetitionEntries.Add(new SpecterCompetitionResultEntry(currentEntry));
            }
        }
    }

}
