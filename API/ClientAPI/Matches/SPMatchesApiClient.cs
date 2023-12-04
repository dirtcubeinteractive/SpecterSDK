using System;
using System.Collections.Generic;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared;
using Newtonsoft.Json;

namespace SpecterSDK.API.ClientAPI.Matches
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
        public Dictionary<string, object> systemParams;
    }
    
    [Serializable]
    public abstract class SPMatchSessionRequestBaseData : SPApiRequestBaseData
    {
        public string matchId;
        public string competitionId;
        public List<SPMatchUserInfo> userInfo;
    }
    
    public partial class SPMatchesApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        public SPMatchesApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}