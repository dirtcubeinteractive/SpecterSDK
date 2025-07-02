using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Players.Me
{
    [Serializable]
    public class SPGetMyInventoryCollectionsResponse : ISpecterApiResponseData
    {
        public List<string> collections { get; set; }
        public int totalCount { get; set; }
    }
}