using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
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
    
    // User authentication account data in SDK responses
    [Serializable]
    public class SPUserAuthAccountData
    {
        public string authProvider { get; set; }
        public string userId { get; set; }
    }

    // Authenticated user data in SDK responses
    [Serializable]
    public class SPAuthenticatedUserResponseData : SPUserProfileResponseData
    {
        public bool createdUser { get; set; }
    }
    
    [Serializable]
    public class SPGetPlayerDataResponseData : Dictionary<string, SPPlayerData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPUpdatePlayerDataResponseData : Dictionary<string, SPPlayerData>, ISpecterApiResponseData { }
    
    [Serializable]
    public class SPRemovePlayerDataResponseData : Dictionary<string, SPPlayerData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPPlayerData
    {
        public object value { get; set; }
        public SPPlayerDataPermission permission { get; set; }
    }

    [Serializable]
    public sealed class SPPlayerDataPermission : SPEnum<SPPlayerDataPermission>
    {
        public static readonly SPPlayerDataPermission Public = new SPPlayerDataPermission(0, nameof(Public).ToLower(), nameof(Public));
        public static readonly SPPlayerDataPermission Private = new SPPlayerDataPermission(1, nameof(Private).ToLower(), nameof(Private));

        private SPPlayerDataPermission(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    #endregion
}
