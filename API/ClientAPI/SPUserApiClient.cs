using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI
{
    public class SPUserApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPUserApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPGetUserProfileResult> GetProfileAsync(SPGetUserProfileRequest request)
        {
            var defaultAttributes = new List<string>()
            {
                nameof(SPUserResponseBaseData.uuid),
                nameof(SPUserResponseBaseData.id),
                nameof(SPUserResponseBaseData.username),
                nameof(SPUserResponseBaseData.hash)
            };

            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();

            var result = await PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/get-profile", AuthType, request);
            return result;
        }

        public async Task<SPUpdateUserProfileResult> UpdateProfileAsync(SPUpdateUserProfileRequest request, Action<SPUpdateUserProfileResult> onComplete = null)
        {
            var task = PutAsync<SPUpdateUserProfileResult, SPGeneralResponseData>("/v1/client/user/update-profile", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));

            var result = await task;
            return result;
        }
    }
}