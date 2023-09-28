using System;
using System.Collections.Generic;
using SpecterSDK.APIDataModels.Interfaces;
using Newtonsoft.Json;

namespace SpecterSDK.APIDataModels
{
    using ObjectModels;
    
    #region Request Data Models

    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProfileRequest : SPApiRequestBaseData
    {
        public string id { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }
    
    public class SPGetUserProfileResult : SPApiResultBase<SPGetUserProfileResult, SPUserProfileResponseData>
    {
        public SpecterUser User;
        
        protected override void LoadFromData(SPUserProfileResponseData data)
        {
            User = SpecterUser.CreateFromData(data);
        }
    }

    [System.Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateUserProfileRequest : SPApiRequestBaseData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string birthdate { get; set; }
        public string customId { get; set; }
        public bool? isKyc { get; set; }
    }
    
    public class SPUpdateUserProfileResult : SPApiResultBase<SPUpdateUserProfileResult, SPGeneralResponseDictionaryData>
    { 
        protected override void LoadFromData(SPGeneralResponseDictionaryData data) { }
    }

    #endregion
    
    #region Response Data Models
    
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
    
    #endregion
}
