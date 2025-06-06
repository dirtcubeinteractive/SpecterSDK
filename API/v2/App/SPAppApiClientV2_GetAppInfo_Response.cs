using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetAppInfoResponse : SPAppInfoData, ISpecterApiResponseData { }

    [Serializable]
    public class SPAppInfoData : ISpecterGameData, ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }

        public string howTo { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> videoUrls { get; set; }
        
        public List<SPAppPlatformData> platforms { get; set; }
        public List<SPLocationData> locations { get; set; }
        public List<SPGameGenreData> genres { get; set; }
        public List<SPAppCategoryData> categories { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}