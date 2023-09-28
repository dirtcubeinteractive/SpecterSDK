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