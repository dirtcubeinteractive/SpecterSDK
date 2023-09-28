using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SpecterSDK.APIDataModels;
using SpecterSDK.APIDataModels.Interfaces;
using SpecterSDK.Shared;
using UnityEngine.SocialPlatforms.Impl;

namespace SpecterSDK.APIClients
{
    using ObjectModels;
    
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProfileRequest : SPApiRequestBase
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
    public class SPUpdateUserProfileRequest : SPApiRequestBase
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
    public class SPUserApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPUserApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPGetUserProfileResult> GetProfile(SPGetUserProfileRequest request)
        {
            List<string> defaultAttributes = new List<string>()
            {
                "id",
                "username",
                "customId",
            };

            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();

            var result = await PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/profile", AuthType, request);
            return result;
        }

        public async Task<SPUpdateUserProfileResult> UpdateProfile(SPUpdateUserProfileRequest request)
        {
            var result = await PutAsync<SPUpdateUserProfileResult, SPGeneralResponseDictionaryData>("/v1/client/user/update", AuthType, request);
            return result;
        }
    }
}