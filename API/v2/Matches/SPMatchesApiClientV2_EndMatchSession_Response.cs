using System;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Matches
{
    [Serializable]
    public class SPEndMatchSessionResponse : ISpecterApiResponseData
    {
        public string matchSessionId { get; set; }
    }
}