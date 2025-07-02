using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.App
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

    public class SPGetDocumentsResult : SpecterApiResultBase<SPGetDocumentsResponse>
    {
        List<SPDocumentInfo> DocumentInfos { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            DocumentInfos = Response.data == null ? new List<SPDocumentInfo>() : Response.data.ConvertAll(x => new SPDocumentInfo(x));
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetDocumentsResult> GetDocumentsAsync(SPGetDocumentsRequest request)
        {
            var result = await PostAsync<SPGetDocumentsResult, SPGetDocumentsResponse>("/v2/client/app/get-documents", AuthType, request);
            return result;
        }
    }
}