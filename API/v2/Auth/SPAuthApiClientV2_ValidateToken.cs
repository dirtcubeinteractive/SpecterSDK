using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to validate an access token.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPValidateTokenRequest : SPApiRequestBase
    {
        /// <summary>
        /// Token identifying the entity (optional).
        /// </summary>
        public string entityToken { get; set; }
        
        /// <summary>
        /// Access token to validate.
        /// </summary>
        public string accessToken { get; set; }
    }

    public class SPValidateTokenResult : SpecterApiResultBase<SPValidateTokenResponse>
    {
        public string AccessToken { get; set; }
        public long Expires { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            AccessToken = Response.data.accessToken;
            Expires = Response.data.expires;
        }
    }

    public partial class SPAuthApiClientV2
    {
        public async Task<SPValidateTokenResult> ValidateTokenAsync(SPValidateTokenRequest request)
        {
            var result = await PostAsync<SPValidateTokenResult, SPValidateTokenResponse>("/v2/client/auth/validate-token", AuthType, request);
            return result;
        }
    }
}