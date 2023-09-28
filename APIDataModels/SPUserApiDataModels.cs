using System;
using System.Collections.Generic;
using SpecterSDK.APIDataModels.Interfaces;

namespace SpecterSDK.APIDataModels
{
    // User authentication account data in SDK responses
    [Serializable]
    public class SPUserAuthAccountData
    {
        public string authProvider { get; set; }
        public string userId { get; set; }
    }
    
    // User data in SDK responses
    [Serializable]
    public abstract class SPUserResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string hash { get; set; }
        public string thumbUrl { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string accessToken { get; set; }
        public string entityToken { get; set; }
        public List<SPUserAuthAccountData> linkedAccounts { get; set; }
    }

    // Authenticated user data in SDK responses
    [Serializable]
    public class SPAuthenticatedUserResponseData : SPUserResponseBaseData
    {
    }

    [Serializable]
    public class SPUserProfileResponseData : SPUserResponseBaseData
    {
        
    }
}
