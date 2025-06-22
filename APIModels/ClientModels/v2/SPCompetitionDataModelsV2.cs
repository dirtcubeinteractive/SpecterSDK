using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPCompetitionConfigData
    {
        public long? minPlayers { get; set; }
        public long? maxPlayers { get; set; }
        public long? maxEntryAllowed { get; set; }
        public long? maxAttemptAllowed { get; set; }
    }

    [Serializable]
    public class SPCompetitionEntryDataV2
    {
        public string entryId { get; set; }
        public long numberOfAttemptsLeft { get; set; }
        public SPCompetitionEntryStatus status { get; set; }
        public SPInstanceScheduleData instanceSchedule { get; set; }
    }

    [Serializable]
    public class SPInstantBattleHistoryEntryData : ISpecterResourceData, ISpecterCompetitionInfoData
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