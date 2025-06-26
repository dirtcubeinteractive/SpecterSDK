using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Competitions
{
    /// <summary>
    /// Represents a request to enter a competition.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPEnterCompetitionRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the competition.
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// Optional instance ID for a specific competition instance.
        /// </summary>
        public string competitionInstanceId { get; set; }
        
        /// <summary>
        /// Additional custom parameters for the competition entry.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPEnterCompetitionResult : SpecterApiResultBase<SPEnterCompetitionResponse>
    {
        public string EntryId { get; set; }
        public string CompetitionInstanceId { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            EntryId = Response.data.entryId;
            CompetitionInstanceId = Response.data.competitionInstanceId;
        }
    }

    public partial class SPCompetitionsApiClientV2
    {
        public async Task<SPEnterCompetitionResult> EnterCompetitionAsync(SPEnterCompetitionRequest request)
        {
            var result = await PostAsync<SPEnterCompetitionResult, SPEnterCompetitionResponse>("/v2/client/competitions/enter-competition", AuthType, request);
            return result;
        }
    }
}