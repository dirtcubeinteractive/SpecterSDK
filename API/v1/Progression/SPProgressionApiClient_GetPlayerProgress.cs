using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Progression
{
    /// <summary>
    /// Represents the request structure to get the user's progress across all progression markers and their associated progression
    /// systems as configured by you on the Specter dashboard.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="SPGetUserProgressRequest"/> class represents a request to get a user's progress from the Specter Progression API.
    /// It can be used to specify the filter criteria for the progression marker progresses to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetUserProgressAsync method in the <see cref="SPProgressionApiClient"/> class to retrieve the user progress information from the API.
    /// </para>
    /// </remarks>
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProgressRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Id of another user whose progress is supposed to be fetched.
        /// Leave null if fetching for currently logged in user
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Represents a list of progression marker IDs used as filter criteria for retrieving progress information from the Specter Progression API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The progressionMarkerIds property is used in the SPGetUserProgressRequest class to specify the specific progression marker IDs for filtering the retrieved markers and the user's progress
        /// within their associated progression systems from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique item ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> progressionMarkerIds { get; set; }
        
        /// <summary>
        /// Represents the field by which the retrieved progression marker details should be sorted.
        /// </summary>
        /// <remarks>
        /// The sortField property is used in the <see cref="SPGetUserProgressRequest"/> class
        /// to specify the field by which the user's progression marker progress details should be sorted when retrieved
        /// from the Specter Inventory API.
        /// </remarks>
        /// <example>sortField = "name"</example>
        public string sortField { get; set; }
        
        /// <summary>
        /// Represents the sort order for the user's progression marker details.
        /// Possible values are "asc" for ascending order and "desc" for descending order.
        /// </summary>
        /// <remarks>
        /// The sort order can be set in the SPGetUserProgressRequest class to specify the sort order of the progression marker progress details to be returned from the Specter Progression API.
        /// </remarks>
        /// <example>
        /// <code>
        /// SPGetUserProgressRequest request = new SPGetUserProgressRequest();
        /// request.sortOrder = "desc";
        /// </code>
        /// </example>
        public string sortOrder { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetUserProgressAsync method in the <see cref="SPProgressionApiClient"/> class.
    /// </summary>
    public class SPGetUserProgressResult : SpecterApiResultBase<SPUserProgressDataList>
    {
        // List of progression marker specific details of the user's progress.
        public List<SpecterUserProgress> Progressions;
        
        protected override void InitSpecterObjectsInternal()
        {
            Progressions = new List<SpecterUserProgress>();
            foreach (var progressionData in Response.data)
            {
                Progressions.Add(new SpecterUserProgress(progressionData));  
            }
        }
    }

    public partial class SPProgressionApiClient
    {
        /// <summary>
        /// Get the user progress asynchronously.
        /// <remarks>
        /// For full information about the get user progress endpoint, see the Progression section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>
        /// </remarks>
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetUserProgressRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetUserProgressResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetUserProgressResult> GetUserProgressAsync(SPGetUserProgressRequest request)
        {
            var result = await PostAsync<SPGetUserProgressResult, SPUserProgressDataList>("/v1/client/progression/get-progress", AuthType, request);
            return result;
        }

    }
}