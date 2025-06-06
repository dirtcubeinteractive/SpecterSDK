using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.Matches
{
    /// <summary>
    /// Represents the request to start a match session.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPStartMatchSessionRequest : SPMatchSessionRequestBase
    {
        /// <summary>
        /// The ID of the match session to start.
        /// <remarks>
        /// Only applicable if create match session was called prior to starting.
        /// Otherwise, only the other properties inherited from <see cref="SPMatchSessionRequestBase"/> are sufficient.
        /// </remarks>
        /// </summary>
        public string matchSessionId { get; set; }
    }
    
    /// <summary>
    /// Represents the result of starting a match session.
    /// </summary>
    public class SPStartMatchSessionResult : SpecterApiResultBase<SPMatchSessionResponseData>
    {
        // The ID of the match session that was started and/or created.
        public string MatchSessionId;

        protected override void InitSpecterObjectsInternal()
        {
            MatchSessionId = Response.data.matchSessionId;
        }
    }

    public partial class SPMatchesApiClient
    {
        /// <summary>
        /// Asynchronously starts a match session and returns the result.
        /// </summary>
        /// <param name="request"><see cref="SPStartMatchSessionRequest"/> containing details about the match session to be started.</param>
        /// <returns>A Task representing the asynchronous operation, with <see cref="SPStartMatchSessionResult"/> as the result type.</returns>
        public async Task<SPStartMatchSessionResult> StartMatchSessionAsync(SPStartMatchSessionRequest request)
        {
            var result = await PostAsync<SPStartMatchSessionResult, SPMatchSessionResponseData>("/v1/client/matches/start-session", AuthType, request);
            return result;
        }

    }

}
