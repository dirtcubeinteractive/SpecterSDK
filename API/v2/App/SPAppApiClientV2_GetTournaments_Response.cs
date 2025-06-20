using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;
using SPPrizeDistributionData = SpecterSDK.APIModels.ClientModels.v2.SPPrizeDistributionData;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetTournamentsResponse : ISpecterMasterResponse
    {
        public List<SPTournamentData> tournaments { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPTournamentData : ISpecterResourceData, ISpecterMasterData, ISpecterCompetitionData, ISpecterCompetitionInfoData, ISpecterUnlockableData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public SPScheduleData schedule { get; set; }
        
        public SPCompetitionConfigData config { get; set; }
        public SPCompetitionFormatData type { get; set; }
        public List<SPEntryFeeDataV2> entryFees { get; set; }
        
        public SPLeaderboardSourceData source { get; set; }
        public SPMatchResourceData match { get; set; }
        public SPPrizeDistributionData prizeDistribution { get; set; }
        public SPLeaderboardRankingMethodData rankingMethod { get; set; }
        
        public SPUnlockConditionsData unlockConditions { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}