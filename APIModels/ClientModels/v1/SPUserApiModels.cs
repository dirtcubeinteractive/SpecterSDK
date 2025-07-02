using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels.v1
{
    #region Api Data Models

    [Serializable]
    public class SPUserAuthResponseData : ISpecterApiResponseData
    {
        public SPUserProfileResponseData user { get; set; }
        public string accessToken { get; set; }
        public string entityToken { get; set; }
        public bool createdAccount { get; set; }
    }
    
    [Serializable]
    public class SPUserProfileResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string thumbUrl { get; set; }
    }
    
    // User data in SDK responses
    [Serializable]
    public class SPUserProfileResponseData : SPUserProfileResponseBaseData
    {
        public string hash { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public List<SPUserAuthAccountData> linkedAccounts { get; set; }
    }

    // Authenticated user data in SDK responses
    [Serializable]
    public class SPAuthenticatedUserResponseData : SPUserProfileResponseData
    {
        public bool createdUser { get; set; }
    }
    
    [Serializable]
    public class SPGetPlayerDataResponseData : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPUpdatePlayerDataResponseData : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData { }
    
    [Serializable]
    public class SPRemovePlayerDataResponseData : Dictionary<string, SPPlayerDataEntryData>, ISpecterApiResponseData { }

    #endregion
}
