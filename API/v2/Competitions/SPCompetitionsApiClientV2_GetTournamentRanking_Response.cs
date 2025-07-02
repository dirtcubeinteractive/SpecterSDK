using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.Players.Others;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    [Serializable]
    public class SPGetTournamentRankingResponse : SPCompetitionRankingData, ISpecterApiResponseData
    {
        public int? totalRanks { get; set; }
    }

    [Serializable]
    public class SPCompetitionRankingData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public List<SPCompetitionParticipantRankingData> currentPlayerRanks { get; set; }
        public List<SPCompetitionParticipantRankingData> rankings { get; set; }
    }
}