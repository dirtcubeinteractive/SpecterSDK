using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v1.Events
{
    /// <summary>
    /// Represents a request to send custom events to the Specter API.
    /// This custom event can be used to track user actions, interactions or 
    /// any app-specific events that are significant within the context of the application.
    /// Custom events are set up by you on the dashboard.
    /// Refer to <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/events/events-configuration">Events Configuration</a>
    /// in the Specter User Manual for information about setting up custom events.
    /// </summary>
    [Serializable]
    public class SPSendCustomEventsRequest : SPApiRequestBase, ISpecterEventConfigurable
    {
        /// <summary>
        /// Represents the event ID for sending custom events to the Specter API.
        /// This request property is required.
        /// </summary>
        public string eventId { get; set; }

        /// <summary>
        /// A collection of additional parameters specific to the Specter system.
        /// This request property is optional.
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
        
        /// <summary>
        /// Custom parameters you configured on the dashboard relevant to the event.
        /// This request property is optional.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents the result of sending custom events using the Specter API.
    /// </summary>
    public class SPSendCustomEventsResult: SpecterApiResultBase<SPGeneralResponseData>
    {
        /// <summary>
        /// The custom event endpoint only returns a simple success message.
        /// </summary>
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }

    public partial class SPEventsApiClient
    {
        /// <summary>
        /// Sends the specific custom event to the Specter server.
        /// This function takes in a request with custom event-related information and sends it to the Specter server.
        /// </summary>
        /// <param name="request">The SPSendCustomEventsRequest containing the custom event's data. See <see cref="SPSendCustomEventsRequest"/> for the structure of the request object.</param> 
        /// <returns>A Task representing the operation of sending the custom event. The result of the task will be a <see cref="SPSendCustomEventsResult"/> object.</returns>
        public async Task<SPSendCustomEventsResult> SendCustomEvents(SPSendCustomEventsRequest request)
        {
            var result = await PostAsync<SPSendCustomEventsResult, SPGeneralResponseData>("/v1/client/events/send-custom", AuthType, request); ;
            return result;
        }
    }
}
