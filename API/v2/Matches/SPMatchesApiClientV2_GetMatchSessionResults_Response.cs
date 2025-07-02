using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Matches
{
    [Serializable]
    public class SPGetMatchSessionResultsResponse : List<SPMatchSessionResultData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPMatchSessionResultData
    {
        public string matchSessionId { get; set; }
        
        public SPMatchResourceData match { get; set; }
        public SPGameResourceData game { get; set; }
        public SPCompetitionResourceData competition { get; set; }
        
        public List<SPMatchSessionPlayerInfoData> userInfo { get; set; }
        
        public DateTime? playedAt { get; set; }
    }
}