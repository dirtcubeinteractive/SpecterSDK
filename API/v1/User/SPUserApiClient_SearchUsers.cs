using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.User
{
    /// <summary>
    /// Represents a request to search for users.
    /// </summary>
    [Serializable]
    public class SPSearchUsersRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Search string which searches for users by the value specified in searchBy
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Specify the filter type. Currently limited to "username"
        /// </summary>
        public string searchBy { get; set; }
    }

    /// <summary>
    /// Represents the result of a user search operation in the Specter SDK.
    /// </summary>
    public class SPSearchUsersResult : SpecterApiResultBase<SPResponseDataList<SPUserProfileResponseBaseData>>
    {
        /// <summary>
        /// List of fetched min user info. To get additional details about the users, call Get User Profile
        /// using the IDs of the users retrieved by the search operation.
        /// </summary>
        public List<SpecterUserBase> Users { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Users = new List<SpecterUserBase>();
            foreach (var user in Response.data)
            {
                Users.Add(new SpecterUserBase(user));
            }
        }
    }

    public partial class SPUserApiClient
    {
        /// <summary>
        /// Searches for users asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPSearchUsersRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPSearchUsersResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPSearchUsersResult> SearchUsersAsync(SPSearchUsersRequest request)
        {
            var result = await PostAsync<SPSearchUsersResult, SPResponseDataList<SPUserProfileResponseBaseData>>("/v1/client/user/search", AuthType, request);
            return result;
        }
    }
}