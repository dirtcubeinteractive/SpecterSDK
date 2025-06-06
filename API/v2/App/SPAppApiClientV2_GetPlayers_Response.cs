using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetPlayersResponse : List<SPPlayerProfileData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPPlayerProfileData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string displayName { get; set; }
        public string username { get; set; }
        public string thumbUrl { get; set; }
        
        public string customId { get; set; }
        public string email { get; set; }
    }
}