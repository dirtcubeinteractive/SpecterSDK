using System;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Matches
{
    [Serializable]
    public class SPStartMatchSessionResponse : ISpecterApiResponseData
    {
        public string matchSessionId { get; set; }
    }
}