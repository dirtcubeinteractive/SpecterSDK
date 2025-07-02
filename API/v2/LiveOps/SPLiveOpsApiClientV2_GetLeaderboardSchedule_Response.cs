using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.LiveOps
{
    [Serializable]
    public class SPGetLeaderboardScheduleResponse : SPScheduleData, ISpecterApiResponseData
    {
        public SPLeaderboardResourceData leaderboard { get; set; }
        public List<SPInstanceScheduleData> instances { get; set; }
    }
}