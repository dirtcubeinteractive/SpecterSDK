using System;
using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App.DTOs;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    [Serializable]
    public class SPGetGamesResponse : ISpecterMasterResponse
    {
        public List<SPGameData> games { get; set; }
        public int totalCount { get; set; }
        public DateTime? lastUpdate { get; set; }
    }

    [Serializable]
    public class SPGameData : ISpecterGameData, ISpecterMasterData
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
        
        public bool isScreenOrientationLandscape { get; set; }
        public List<SPMatchResourceData> matches { get; set; }
        
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }
}