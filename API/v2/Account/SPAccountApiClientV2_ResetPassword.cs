using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to reset the user's password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPResetPasswordRequest : SPApiRequestBase
    {
        /// <summary>
        /// User's identifier, either email or username, depending on the 'type' provided.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Specifies if 'id' refers to an email or username.
        /// </summary>
        public SPAccountAuthIdType type { get; set; }
        
        /// <summary>
        /// The token provided to the user for resetting their password.
        /// </summary>
        public string resetPasswordToken { get; set; }
        
        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public string password { get; set; }
    }

    public class SPResetPasswordResult : SpecterApiResultBase<SPResetPasswordResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }
    
    public partial class SPAccountApiClientV2
    {
        public async Task<SPResetPasswordResult> ResetPasswordAsync(SPResetPasswordRequest request)
        {
            var result = await PostAsync<SPResetPasswordResult, SPResetPasswordResponse>("/v2/client/account/reset-password", AuthType, request);
            return result;
        }
    }
}