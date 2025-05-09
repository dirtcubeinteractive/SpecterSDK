using System;
using System.Collections.Generic;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPAppInfoResponseData : SPResourceResponseData, ISpecterMasterData
    {
        public string howTo { get; set; }
        public List<string> screenshotUrls { get; set; }
        public List<string> videoUrls { get; set; }
        public SPAppCategoryData categories { get; set; }
        public List<SPAppPlatformData> platforms { get; set; }
        public List<SPLocationData> countries { get; set; }
        public List<SPGameGenreData> genre { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPAppCategoryData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}