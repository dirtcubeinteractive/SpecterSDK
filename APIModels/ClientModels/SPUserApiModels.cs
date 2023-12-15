using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Api Data Models

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
    
    // User authentication account data in SDK responses
    [Serializable]
    public class SPUserAuthAccountData
    {
        public string authProvider { get; set; }
        public string userId { get; set; }
    }
    
    [Serializable]
    public class SPUserProfileResponseData : SPUserResponseBaseData
    {
    }

    // Authenticated user data in SDK responses
    [Serializable]
    public class SPAuthenticatedUserResponseData : SPUserProfileResponseData
    {
    }
    

    [Serializable]
    public class SPGetPlayerDataResponseData : SPResponseDataDictionary<string,SPPlayerDataResponseData> { }

    [Serializable]
    public class SPUpdatePlayerDataResponseData : SPResponseDataDictionary<string,SPPlayerDataResponseData> { }
    
    [Serializable]
    public class SPRemovePlayerDataResponseData : SPResponseDataDictionary<string,SPPlayerDataResponseData> { }

    [Serializable]
    public class SPPlayerDataResponseData : ISpecterApiResponseData
    {
        public string value;
        public SPPlayerDataEntryPermission permission;
    }

    [Serializable]
    public sealed class SPPlayerDataEntryPermission : SPEnum<SPPlayerDataEntryPermission>
    {
        public static readonly SPPlayerDataEntryPermission Public = new SPPlayerDataEntryPermission(0, nameof(Public).ToLower(), nameof(Public));
        public static readonly SPPlayerDataEntryPermission Private = new SPPlayerDataEntryPermission(1, nameof(Private).ToLower(), nameof(Private));

        private SPPlayerDataEntryPermission(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    #endregion
}
