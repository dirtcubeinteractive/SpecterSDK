using System;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Competitions
{
    [Serializable]
    public class SPCheckAttemptsResponse : ISpecterApiResponseData
    {
        public int numberOfAttemptsLeft { get; set; }
    }
}