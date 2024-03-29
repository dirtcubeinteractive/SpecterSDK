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
        public DateTime StartDate;
        public DateTime? EndDate;
        public SPLeaderboardInterval RecurrenceType;
        public SPCompetitionStatus Status;
        public SPCompetitionFormat Format;
        public SPCompetitionMatchData MatchData;
        public SPCompetitionGameData GameData;

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
            RecurrenceType = data.recurrenceType;
            RecurrenceCount = data.recurrenceCount ?? 1;
            StartDate = data.startDate;
            EndDate = data.endDate;
        }
    }

    public class SpecterCompetition : SpecterCompetitionBase, ISpecterMasterObject
    {
        public List<SPUnlockConditionResponseData> UnlockCondition;

        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterCompetition(SPCompetitionResponseData data) : base(data)
        {
            UnlockCondition = data.unlockConditions;
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
