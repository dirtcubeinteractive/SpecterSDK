using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.LiveOps
{
    /// <summary>
    /// Represents a request to retrieve schedule history for specified competitions.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCompetitionScheduleRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of competition IDs for which to retrieve schedule history.
        /// </summary>
        public List<string> competitionIds { get; set; }
    }
}