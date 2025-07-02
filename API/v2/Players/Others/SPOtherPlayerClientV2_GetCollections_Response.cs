using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Players.Others
{
    [Serializable]
    public class SPGetOtherPlayerInventoryCollectionsResponse : ISpecterApiResponseData
    {
        public List<string> collections { get; set; }
        public int totalCount { get; set; }
    }
}