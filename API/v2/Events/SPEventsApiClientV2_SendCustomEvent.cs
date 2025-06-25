using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.Events
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

    public class SPSendCustomEventResult : SpecterApiResultBase<SPSendCustomEventResponse>
    {
        protected override void InitSpecterObjectsInternal() { }
    }

    public partial class SPEventsApiClientV2
    {
        public async Task<SPSendCustomEventResult> SendCustomEventAsync(SPSendCustomEventRequest request)
        {
            var result = await PostAsync<SPSendCustomEventResult, SPSendCustomEventResponse>("", AuthType, request);
            return result;
        }
    }
}