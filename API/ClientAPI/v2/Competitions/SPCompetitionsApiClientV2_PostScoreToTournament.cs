using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Competitions
{
    /// <summary>
    /// Represents a request to post a score to a tournament.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPostScoreToTournamentRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier of the competition (optional).
        /// </summary>
        public string competitionId { get; set; }
        
        /// <summary>
        /// The unique identifier of the entry to which the score belongs.
        /// </summary>
        public string entryId { get; set; }
        
        /// <summary>
        /// The score to submit for the entry.
        /// </summary>
        public long score { get; set; }
    }
}