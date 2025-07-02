using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to register a new user with a custom ID.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPSignUpWithCustomIdRequest : SPApiRequestBase
    {
        /// <summary>
        /// Custom ID for the account.
        /// </summary>
        public string customId { get; set; }
        
        /// <summary>
        /// Referral code for sign-up (optional).
        /// </summary>
        public string referralCode { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the user (optional).
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    public class SPSignUpWithCustomIdResult : SpecterApiResultBase<SPSignUpWithCustomIdResponse>
    {
        public SPAuthenticatedUser User { get; set; }
        
        public string AccessToken { get; set; }
        public string EntityToken { get; set; }
        public bool CreatedAccount { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            User = new SPAuthenticatedUser(Response.data.user);
            AccessToken = Response.data.accessToken;
            EntityToken = Response.data.entityToken;
            CreatedAccount = Response.data.createdAccount;
        }
    }

    public partial class SPAuthApiClientV2
    {
        public async Task<SPSignUpWithCustomIdResult> SignUpWithCustomIdAsync(SPSignUpWithCustomIdRequest request)
        {
            var result = await PostAsync<SPSignUpWithCustomIdResult, SPSignUpWithCustomIdResponse>("/v2/client/auth/signup-custom", AuthType, request);
            return result;
        }
    }
}