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

        public async Task<SPGetUserProfileResult> GetProfile(SPGetUserProfileRequest request)
        {
            var defaultAttributes = new List<string>()
            {
                nameof(SPUserResponseBaseData.uuid),
                nameof(SPUserResponseBaseData.id),
                nameof(SPUserResponseBaseData.username),
                nameof(SPUserResponseBaseData.linkedAccounts)
            };

            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();

            var result = await PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/get-profile", AuthType, request);
            return result;
        }

        public async Task<SPUpdateUserProfileResult> UpdateProfile(SPUpdateUserProfileRequest request)
        {
            var result = await PutAsync<SPUpdateUserProfileResult, SPGeneralResponseDictionaryData>("/v1/client/user/update-profile", AuthType, request);
            return result;
        }
    }
}