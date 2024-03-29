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
        public SpecterCompetitionBase(SPCompetitionResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            IconUrl = data.iconUrl;
            Description = data.description;       
        }
    }

    public class SpecterCompetition : SpecterCompetitionBase, ISpecterMasterObject
    {
        public int MinPlayers;
        public int MaxPlayers;
        public int MaxEntryAllowed;
        public int MaxAttemptAllowed;
        public SPCompetitionStatus Status;
        public SPCompetitionFormat Format;
        public SPCompetitionMatchData MatchData;
        public SPCompetitionGameData GameData;
        public List<SPUnlockConditionResponseData> UnlockCondition;

        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterCompetition(SPCompetitionResponseData data) : base(data)
        {
            MinPlayers = data.minPlayers;
            MaxPlayers = data.maxPlayers;
            MaxEntryAllowed = data.maxEntryAllowed;
            MaxAttemptAllowed = data.maxAttemptAllowed;
            Status = data.status;
            Format = data.formatType.name;
            MatchData = data.match;
            GameData = data.game;
            UnlockCondition = data.unlockConditions;
        }

    }

}
