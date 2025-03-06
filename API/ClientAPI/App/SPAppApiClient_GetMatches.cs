using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents a request to get matches from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetMatchesRequest class represents a request to get matches from the Specter App API.
    /// It can be used to specify the filter criteria for the matches to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetMatchesAsync method in the SPAppApiClient class to retrieve the matches from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMatchesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of game IDs used as filter criteria for retrieving matches by their associated games from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The gameIds property is used in the SPGetMatchesRequest class to specify the specific game IDs for filtering the retrieved matches by their associated games from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique game ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> gameIds { get; set; }
        
        /// <summary>
        /// Represents a list of match IDs used as filter criteria for retrieving matches from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The matchIds property is used in the SPGetMatchesRequest class to specify the specific match IDs for filtering the retrieved matches from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique match ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> matchIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Additional entities that can be fetched along with the list of matches
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
        
        /// <summary>
        /// Additional attributes of the retrieved matches that you wish to receive (eg: createdAt, updatedAt etc.)
        /// </summary>
        public List<string> attributes { get; set; }
    }
    
    /// <summary>
    /// Represents the result of the GetMatchesAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetMatchesResult : SpecterApiResultBase<SPGetMatchesResponseData>
    {
        // List of all matches fetched
        public List<SpecterMatch> Matches;
        
        // Total count of matches configured on the dashboard
        public int TotalMatchCount;

        protected override void InitSpecterObjectsInternal()
        {
            Matches = new List<SpecterMatch>();
            foreach (var match in Response.data.matches)
            {
                Matches.Add(new SpecterMatch(match));
            }
            TotalMatchCount = Response.data.totalCount;
        }
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of matches asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetMatchesRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetMatchesResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetMatchesResult> GetMatchesAsync(SPGetMatchesRequest request)
        {
            var result = await PostAsync<SPGetMatchesResult, SPGetMatchesResponseData>("/v1/client/app/get-matches", AuthType, request);
            return result;
        }
    }
}