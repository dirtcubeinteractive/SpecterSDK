using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Auth
{
    /// <summary>
    /// Represents a request to authenticate a user using email and password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPLoginWithEmailRequest : SPApiRequestBase
    {
        /// <summary>
        /// User's email address.
        /// </summary>
        public string email { get; set; }
        
        /// <summary>
        /// Password for the account.
        /// </summary>
        public string password { get; set; }
        
        /// <summary>
        /// Flag to create an account if it doesn't exist.
        /// </summary>
        public bool? createAccount { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the user (optional).
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
    
    public class SPLoginWithEmailResult : SpecterApiResultBase<SPLoginWithEmailResponse>
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
        public async Task<SPLoginWithEmailResult> LoginWithEmailAsync(SPLoginWithEmailRequest request)
        {
            var result = await PostAsync<SPLoginWithEmailResult, SPLoginWithEmailResponse>("/v2/client/auth/login-email", AuthType, request);
            return result;
        }
    }
}