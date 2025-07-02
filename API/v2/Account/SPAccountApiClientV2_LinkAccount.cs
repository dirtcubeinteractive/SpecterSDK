using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to link an account to the user's profile.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPLinkAccountRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specifies the type of account to link, such as a custom ID, Google, or Facebook account.
        /// </summary>
        public SPAccountAuthProvider type { get; set; }
        
        /// <summary>
        /// The unique identifier of the account to be linked, specific to the account type.
        /// </summary>
        public string id { get; set; }
    }

    public class SPLinkAccountResult : SpecterApiResultBase<SPLinkAccountResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAccountApiClientV2
    {
        public async Task<SPLinkAccountResult> LinkAccountAsync(SPLinkAccountRequest request)
        {
            var result = await PostAsync<SPLinkAccountResult, SPLinkAccountResponse>("/v2/client/account/link", AuthType, request);
            return result;
        }
    }
}