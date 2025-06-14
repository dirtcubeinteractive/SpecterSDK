using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents a request to retrieve a specific document's content from Specter.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetDocumentContentRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique identifier for the document to retrieve.
        /// </summary>
        public string documentId { get; set; }
    }

    public class SPGetDocumentContentResult : SpecterApiResultBase<SPGetDocumentContentResponse>
    {
        public string DocumentContent { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            DocumentContent = Response.data?.documentContent;
        }
    }
    
    public partial class SPAppApiClientV2
    {
        public async Task<SPGetDocumentContentResult> GetDocumentContentAsync(SPGetDocumentContentRequest request)
        {
            var result = await PostAsync<SPGetDocumentContentResult, SPGetDocumentContentResponse>("/v2/client/app/get-document-content", AuthType, request);
            return result;
        }
    }
}