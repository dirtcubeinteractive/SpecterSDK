using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetStoreCategoriesResponse : List<SPStoreCategoryData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPStoreCategoryData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        
        public bool isDefault { get; set; }
        public int contentsCount { get; set; }
    }
}