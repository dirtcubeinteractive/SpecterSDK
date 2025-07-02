using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v1.Matches
{
    /// <summary>
    /// Represents the request to create a match session. Creating a match session only uses
    /// the properties which it inherits from the base class <see cref="SPMatchSessionRequestBase"/>.
    /// </summary>
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCreateMatchSessionRequest : SPMatchSessionRequestBase { }
    
    /// <summary>
    /// Represents the result of creating a match session.
    /// </summary>
    public class SPCreateMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        // The ID of the newly created match session.
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        /// <summary>
        /// Asynchronously creates a match session and returns the result.
        /// <remarks>
        /// This API call is only needed to be made if you wish to create a match session before players actually begin play
        /// (e.g. before the start of a match in a multiplayer game).
        /// In most cases, you would simply call the start match session endpoint when a match begins since
        /// starting a match session internally creates a session if one doesn't exist.
        /// </remarks>
        /// </summary>
        /// <param name="request"><see cref="SPCreateMatchSessionRequest"/> containing details about the match session to be created.</param>
        /// <returns>A Task representing the asynchronous operation, with <see cref="SPCreateMatchSessionResult"/> as the result type.</returns>
        public async Task<SPCreateMatchSessionResult> CreateMatchSessionAsync(SPCreateMatchSessionRequest request)
        {
            var result = await PostAsync<SPCreateMatchSessionResult, SPMatchSessionResponseData>("/v1/client/matches/create-session", AuthType, request);
            return result;
        }

    }

}
