using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPGetOtherPlayerTaskProgressResponse : ISpecterApiResponseData
    {
        public List<SPTaskProgressData> taskProgresses { get; set; }
        public int totalCount { get; set; }
    }
}