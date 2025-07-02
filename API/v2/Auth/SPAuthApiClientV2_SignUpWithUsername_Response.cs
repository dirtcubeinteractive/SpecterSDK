using System;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.API.v2.Auth
{
    [Serializable]
    public class SPSignUpWithUsernameResponse : ISpecterApiResponseData
    {
        public SPAuthenticatedUserData user { get; set; }
        
        public string accessToken { get; set; }
        public string entityToken { get; set; }
        public bool createdAccount { get; set; }
    }
}