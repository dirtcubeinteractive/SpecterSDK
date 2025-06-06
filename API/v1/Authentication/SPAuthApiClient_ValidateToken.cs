using System;
using System.Threading.Tasks;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Authentication
{
    /// <summary>
    /// Represents a request to validate an access token.
    /// </summary>
    [Serializable]
    public class SPValidateAccessTokenRequest : SPApiRequestBase
    {
        /// <summary>
        /// The access token to be validated.
        /// </summary>
        public string accessToken;

        /// <summary>
        /// Represents an entity token for authentication purposes.
        /// </summary>
        public string entityToken;
    }

    /// <summary>
    /// Represents the response data for validating an access token.
    /// </summary>
    [Serializable]
    public class SPValidateAccessTokenResponseData: ISpecterApiResponseData
    {
        /// <summary>
        /// The access token that was validated.
        /// </summary>
        public string accessToken { get; set; }
        
        /// <summary>
        /// The UNIX timestamp in seconds indicating when the access token will expire.
        /// </summary>
        public long expires { get; set; }
    }

    /// <summary>
    /// Represents the result of validating an access token.
    /// </summary>
    public class SPValidateAccessTokenResult: SpecterApiResultBase<SPValidateAccessTokenResponseData>
    {
        public string AccessToken;

        protected override void InitSpecterObjectsInternal()
        {
            AccessToken = Response.data.accessToken;
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