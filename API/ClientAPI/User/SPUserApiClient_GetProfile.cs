using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.User
{
    /// <summary>
    /// Represents the request object for getting the user profile information.
    /// <seealso cref="SPUserProfileResponseData"/>
    /// </summary>
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// ID of the user whose profile is to be fetched. Leave null if fetching for current user.
        /// </summary>
        public string id { get; set; }
        
        /// <summary>
        /// Specific attributes of the user to be fetched. Leave null if all provided attributes are to be fetched
        /// <example>
        /// "id", "name", "username", etc.
        /// </example>
        /// </summary>
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// Additional entities that can be fetched along with the user profile.
        /// <remarks>
        /// For list of possible entities see the Get User Profile route in the User section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
    }

    /// <summary>
    /// Represents the result of a get user profile operation.
    /// </summary>
    public class SPGetUserProfileResult : SpecterApiResultBase<SPUserProfileResponseData>
    {
        // The retrieved user profile.
        public SpecterUser User { get; set; }

        protected override void InitSpecterObjectsInternal()
        {
            if (Response.data == null)
                return;
            
            User = new SpecterUser(Response.data);
        }
    }
    
    public partial class SPUserApiClient
    {
        /// <summary>
        /// Gets the user profile asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetUserProfileRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetUserProfileResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetUserProfileResult> GetProfileAsync(SPGetUserProfileRequest request)
        {
            var result = await PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/get-profile", AuthType, request);
            return result;
        }
    }
}