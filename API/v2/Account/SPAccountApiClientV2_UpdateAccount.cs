using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.API.v2.Account
{
    /// <summary>
    /// Represents a request to update a user's account.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateAccountRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specifies the type of account to update, such as a custom ID, Google, or Facebook account.
        /// </summary>
        public SPAccountAuthProvider type { get; set; }
        
        /// <summary>
        /// The unique identifier of the account to be updated, specific to the account type.
        /// </summary>
        public string id { get; set; }
    }

    public class SPUpdateAccountResult : SpecterApiResultBase<SPUpdateAccountResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPAccountApiClientV2
    {
        public async Task<SPUpdateAccountResult> UpdateAccountAsync(SPUpdateAccountRequest request)
        {
            var result = await PostAsync<SPUpdateAccountResult, SPUpdateAccountResponse>("/v2/client/account/update", AuthType, request);
            return result;
        }
    }
}