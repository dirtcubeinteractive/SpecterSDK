using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to change the user's password.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPChangePasswordRequest : SPApiRequestBase
    {
        /// <summary>
        /// The user's current password.
        /// </summary>
        public string currentPassword { get; set; }
        
        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public string newPassword { get; set; }
    }

    public class SPChangePasswordResult : SpecterApiResultBase<SPChangePasswordResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAccountApiClientV2
    {
        public async Task<SPChangePasswordResult> ChangePasswordAsync(SPChangePasswordRequest request)
        {
            var result = await PostAsync<SPChangePasswordResult, SPChangePasswordResponse>("/v2/client/account/change-password", AuthType, request);
            return result;
        }
    }
}