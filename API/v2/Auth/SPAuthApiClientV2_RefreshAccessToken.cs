using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to refresh an access token using an entity token and an expiring token.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRefreshAccessTokenRequest : SPApiRequestBase
    {
        /// <summary>
        /// Token identifying the entity.
        /// </summary>
        public string entityToken { get; set; }
        
        /// <summary>
        /// The access token that is about to expire.
        /// </summary>
        public string expiringAccessToken { get; set; }
    }

    public class SPRefreshAccessTokenResult : SpecterApiResultBase<SPRefreshAccessTokenResponse>
    {
        public string AccessToken { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            AccessToken = Response.data.accessToken;
        }
    }

    public partial class SPAuthApiClientV2
    {
        public async Task<SPRefreshAccessTokenResult> RefreshAccessTokenAsync(SPRefreshAccessTokenRequest request)
        {
            var result = await PostAsync<SPRefreshAccessTokenResult, SPRefreshAccessTokenResponse>("/v2/client/auth/refresh-token", AuthType, request);
            return result;
        }
    }
}