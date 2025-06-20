using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyTournamentHistoryResponse : List<SPTournamentHistoryEntryData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPTournamentHistoryEntryData : ISpecterResourceData, ISpecterCompetitionInfoData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPLeaderboardSourceData source { get; set; }
        public SPMatchResourceData match { get; set; }
        public SPCompetitionConfigData config { get; set; }
        public SPCompetitionFormatData type { get; set; }
        
        public List<SPCompetitionEntryDataV2> entryDetails { get; set; }
    }
}