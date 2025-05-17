using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
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