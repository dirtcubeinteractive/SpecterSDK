using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.Authentication
{
    [Serializable]
    public class SPValidateAccessTokenRequest : SPApiRequestBase
    {
        public string accessToken;
        public string entityToken;
    }

    public class SPValidateAccessTokenResponseData: ISpecterApiResponseData
    {
        public string AccessToken;
    }

    public class SPValidateAccessTokenResult: SpecterApiResultBase<SPValidateAccessTokenResponseData>
    {
        public string AccessToken;

        protected override void InitSpecterObjectsInternal()
        {
            AccessToken = Response.data.AccessToken;
        }
    }

    public partial class SPAuthApiClient
    {
        public async Task<SPValidateAccessTokenResult> ValidateAccessToken(SPValidateAccessTokenRequest request)
        {
            var result = await PostAsync<SPValidateAccessTokenResult, SPValidateAccessTokenResponseData>("/v1/client/auth/validate-token", AuthType, request);
            return result;
        }
    }
    
}