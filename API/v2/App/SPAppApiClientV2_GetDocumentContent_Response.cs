using System;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.App
{
    [Serializable]
    public class SPGetDocumentContentResponse : SPDocumentContentData, ISpecterApiResponseData { }

    [Serializable]
    public class SPDocumentContentData
    {
        public string documentContent { get; set; }
    }
}