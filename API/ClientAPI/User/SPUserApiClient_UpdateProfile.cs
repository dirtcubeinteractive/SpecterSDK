using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.User
{
    /// <summary>
    /// Represents a request to update a user's profile.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateUserProfileRequest : SPApiRequestBase
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        /// <summary>
        /// A unique username for the user.
        /// </summary>
        public string username { get; set; }
        
        /// <summary>
        /// The display name for the user. Non-unique.
        /// </summary>
        public string displayName { get; set; }
        
        /// <summary>
        /// Url for the profile image of the user.
        /// </summary>
        public string thumbUrl { get; set; }
        
        /// <summary>
        /// Flag to indicate if a user has completed their KYC
        /// </summary>
        public bool? isKycComplete { get; set; }
        
        /// <summary>
        /// Any custom metadata unrestricted by permissions that you wish to set.
        /// </summary>
        public Dictionary<string, object> meta { get; set; }
        
        /// <summary>
        /// Custom tags to group a user if needed.
        /// </summary>
        public List<string> tags { get; set; }
    }

    /// <summary>
    /// Represents the result of updating a user's profile.
    /// </summary>
    public class SPUpdateUserProfileResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        /// <summary>
        /// This response returns a simple success message, so no specific object is expected.
        /// </summary>
        public Dictionary<string, object> ObjectDict;
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
    
    public partial class SPUserApiClient
    {
        /// <summary>
        /// Update the user's profile asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPUpdateUserProfileRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPUpdateUserProfileResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPUpdateUserProfileResult> UpdateProfileAsync(SPUpdateUserProfileRequest request)
        {
            var result = await PostAsync<SPUpdateUserProfileResult, SPGeneralResponseData>("/v1/client/user/update-profile", AuthType, request);
            return result;
        }
    }
}