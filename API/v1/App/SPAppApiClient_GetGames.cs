using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.App
{
    /// <summary>
    /// Represents a request to get games from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetGamesRequest class represents a request to get games from the Specter App API.
    /// It can be used to specify the filter criteria for the games to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetGamesAsync method in the SPAppApiClient class to retrieve the games from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetGamesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of game IDs used as filter criteria for retrieving games from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The gameIds property is used in the SPGetGamesRequest class to specify the specific game IDs for filtering the retrieved games from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique game ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> gameIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }

        /// <summary>
        /// Filter to search for and retrieve games by their name.
        /// Multiple games can be fetched by setting a name substring
        /// <example>
        /// Games named Surf Pro and Subway Surfer can be fetched by searching for 'surf' or 'Surf' </example>
        /// </summary>
        public string search { get; set; }

        /// <summary>
        /// Additional entities that can be fetched along with the list of games
        /// </summary>
        public List<SPApiRequestEntity> entities { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetGamesAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetGamesResult : SpecterApiResultBase<SPGetGamesResponseData>
    {
        // List of all games fetched
        public List<SpecterGame> Games;

        // Total count of games configured on the dashboard
        public int TotalGameCount;

        protected override void InitSpecterObjectsInternal()
        {
            Games = new List<SpecterGame>();
            foreach (var game in Response.data.games)
            {
                Games.Add(new SpecterGame(game));
            }

            TotalGameCount = Response.data.totalCount;
        }
    }

    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of games asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetGamesRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetGamesResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetGamesResult> GetGamesAsync(SPGetGamesRequest request)
        {
            var result =
                await PostAsync<SPGetGamesResult, SPGetGamesResponseData>("/v1/client/app/get-games", AuthType,
                    request);
            return result;
        }
    }
}