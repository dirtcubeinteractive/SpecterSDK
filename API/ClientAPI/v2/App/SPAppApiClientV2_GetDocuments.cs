using System;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents a request to retrieve a specific document from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetDocumentsRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the document to retrieve.
        /// </summary>
        public string documentId { get; set; }
    }
}