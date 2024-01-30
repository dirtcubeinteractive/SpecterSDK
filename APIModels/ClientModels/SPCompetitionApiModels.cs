using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK.APIModels.ClientModels
{
    public class SPGetCompetitionsResponseData : ISpecterApiResponseData
    {
        public List<SPCompetitionResponseData> competitions { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPCompetitionResponseData : SPResourceResponseData, ISpecterMasterData
    {
        public int minPlayers { get; set; }
        public int maxPlayers { get; set; }        
        public int maxEntryAllowed {  get; set; }
        public int maxAttemptAllowed { get; set; }
        public int numberOfWinners { get; set; }
        public SPCompetitionStatusType status { get; set; }
        public SPCompetitionFormatData formatType { get; set; }
        public SPCompetitionMatchData match { get; set; }
        public SPCompetitionGameData game { get; set; }

        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }


        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    [Serializable]
    public class SPCompetitionFormatData
    {
        public int id { get; set; }

        public SPCompetitionFormatType name { get; set; }
    }

    [Serializable]
    public class SPCompetitionMatchData : SPResourceResponseData
    {

    }

    [Serializable]
    public class SPCompetitionGameData : SPResourceResponseData
    {

    }

    [Serializable]
    public class SPCompetitionUnlockUnlockCondition : SPUnlockConditionResponseData
    {

    }
}
