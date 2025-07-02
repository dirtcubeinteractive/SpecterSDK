using System;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Auth
{
    [Serializable]
    public class SPRefreshAccessTokenResponse : ISpecterApiResponseData
    {
        public string accessToken { get; set; }
    }
}