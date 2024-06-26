using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPEnterCompetitionResponseData : ISpecterApiResponseData
    {
        public string entryId { get; set; }
    }

    [Serializable]
    public class SPCheckCompetitionAttemptsResponseData : ISpecterApiResponseData
    {
        public int? numberOfAttemptsLeft { get; set; }
    }
    
    [Serializable]
    public class SPGetCompetitionsResponseData : ISpecterApiResponseData
    {
        public List<SPCompetitionResponseData> competitions { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPCompetitionResponseBaseData : SPESportsResourceResponseData
    {
        public int? minPlayers { get; set; }
        public int? maxPlayers { get; set; }
        public int? maxEntryAllowed {  get; set; }
        public int? maxAttemptAllowed { get; set; }
        public SPCompetitionFormatData formatType { get; set; }
        public SPMatchResponseBaseData match { get; set; }
        public SPGameResponseBaseData game { get; set; }
    }
    
    [Serializable]
    public class SPCompetitionResponseData : SPCompetitionResponseBaseData, ISpecterMasterData
    {
        public List<SPPrizeDistributionData> prizeDistributionRules { get; set; }
        public List<SPUnlockConditionResponseData> unlockConditions { get; set; }
        public List<SPEntryFeeData> entryFees { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPCompetitionInstanceResponseData : SPCompetitionResponseBaseData
    {
        public string instanceId { get; set; }
    }

    [Serializable]
    public class SPEnteredCompetitionResponseData : SPCompetitionInstanceResponseData
    {
        public List<SPCompetitionEntryData> entries { get; set; }
    }

    [Serializable]
    public class SPCompetitionEntryData
    {
        public string entryId { get; set; }
        public int? numberOfAttemptsLeft { get; set; }
    }

    [Serializable]
    public class SPCompetitionFormatData
    {
        public int id { get; set; }
        public SPCompetitionFormat name { get; set; }
    }
}
