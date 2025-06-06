using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetMatchesResponse : ISpecterMasterResponse
    {
        public List<SPMatchData> matches { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPMatchData : ISpecterResourceData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        /*public int? minPlayers { get; set; }
        public int? maxPlayers { get; set; }*/
        
        public SPGameResourceData game { get; set; }
        public SPMatchFormatData formatType { get; set; }
        public SPGameMatchOutcomeData outcomeType { get; set; }
        public SPMatchWinConditionData winCondition { get; set; }
        
        public List<SPLeaderboardResourceData> leaderboards { get; set; }
        public List<SPCompetitionResourceData> competitions { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}