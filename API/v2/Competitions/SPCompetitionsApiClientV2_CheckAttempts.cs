using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Interfaces;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to check attempts for a competition entry.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPCheckAttemptsRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the entry to check attempts for.
        /// </summary>
        public string entryId { get; set; }
    }

    public class SPCheckAttemptsResult : SpecterApiResultBase<SPCheckAttemptsResponse>
    {
        public int NumberOfAttemptsLeft { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            NumberOfAttemptsLeft = Response.data.numberOfAttemptsLeft;
        }
    }

    public partial class SPCompetitionsApiClientV2
    {
        public async Task<SPCheckAttemptsResult> CheckAttemptsAsync(SPCheckAttemptsRequest request)
        {
            var result = await PostAsync<SPCheckAttemptsResult, SPCheckAttemptsResponse>("/v2/client/competitions/check-attempts", AuthType, request);
            return result;
        }
    }
}