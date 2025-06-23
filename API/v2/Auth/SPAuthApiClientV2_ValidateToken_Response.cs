using System;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Auth
{
    [Serializable]
    public class SPValidateTokenResponse : ISpecterApiResponseData
    {
        public string accessToken { get; set; }
        public long expires { get; set; }
    }
}