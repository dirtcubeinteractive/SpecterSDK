using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.Events
{
    /// <summary>
    /// Represents a request to send a custom event.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPSendCustomEventRequest : SPApiRequestBase
    {
        /// <summary>
        /// Identifier of the event being fired.
        /// </summary>
        public string eventId { get; set; }
        
        /// <summary>
        /// Additional custom parameters for event processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }
}