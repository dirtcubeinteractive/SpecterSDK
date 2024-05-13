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
        public int RecurrenceCount;
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit RecurrenceType;
        public SPCompetitionFormat Format;
        public SPCompetitionMatchData MatchData;
        public SPCompetitionGameData GameData;
        public SPCompetitionStatus Status;
        public SPIntervalUnit IntervalUnit;
        public int IntervalLength;
        public int Occurrences;

        public SpecterCompetitionBase(SPCompetitionResponseBaseData data) : base(data)
        {
            MinPlayers = data.minPlayers ?? 0;
            MaxPlayers = data.maxPlayers;
            MaxEntryAllowed = data.maxEntryAllowed;
            MaxAttemptAllowed = data.maxAttemptAllowed;
            Status = data.status;
            Format = data.formatType.name;
            MatchData = data.match;
            GameData = data.game;
            RecurrenceType = data.intervalUnit;
            RecurrenceCount = data.occurrences;
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
        }
    }

    public class SpecterCompetition : SpecterCompetitionBase, ISpecterMasterObject
    {
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

}
