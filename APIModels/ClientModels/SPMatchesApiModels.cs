using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPMatchUserInfo
    {
        [JsonRequired]
        public string id;
    }

    [Serializable]
    public class SPEndMatchSessionUserInfo : SPMatchUserInfo
    {
        public int outcome;
        public Dictionary<string, object> customParams;
    }
    
    [Serializable]
    public class SPMatchSessionResponseData : ISpecterApiResponseData
    {
        public string matchSessionId { get; set; }
    }
}