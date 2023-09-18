using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpecterSDK.Models;

namespace SpecterSDK.APIClients
{
    [System.Serializable]
    public abstract class SPAuthLoginRequestBase : SPApiRequestBase
    {
        public string projectId { get; set; }
        public bool createAccount { get; set; }
    }
    
    [System.Serializable]
    public class SPAuthLoginCustomIdRequest : SPAuthLoginRequestBase
    {
        public string customId { get; set; }
    }
    
    public class SPAuthApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAuthApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPApiResponse<SPUserProfile>> LoginWithCustomId(SPAuthLoginCustomIdRequest request)
        {
            var response = await PostAsync<SPUserProfile>("/v1/client/auth/login-custom", AuthType, request);
            return response;
        }
    }
}
