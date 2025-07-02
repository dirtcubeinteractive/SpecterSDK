using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetDocumentsResponse : List<SPDocumentData>, ISpecterApiResponseData { }

    [Serializable]
    public class SPDocumentData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}