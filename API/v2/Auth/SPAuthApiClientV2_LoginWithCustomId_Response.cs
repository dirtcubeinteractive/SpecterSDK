using System;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Networking.Interfaces;

namespace SpecterSDK.API.v2.Auth
{
    [Serializable]
    public class SPLoginWithCustomIdResponse : ISpecterApiResponseData
    {
        public SPAuthenticatedUserData user { get; set; }
        
        public string accessToken { get; set; }
        public string entityToken { get; set; }
        public bool createdAccount { get; set; }
    }
}