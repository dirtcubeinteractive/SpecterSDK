using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using SpecterSDK.APIDataModels;
using SpecterSDK.APIDataModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;
using UnityEngine;

namespace SpecterSDK.APIClients
{
    [System.Serializable]
    public abstract class SPAuthLoginRequestBase : SPApiRequestBaseData, IProjectConfigurable
    {
        public string projectId { get; set; }
        public bool createAccount { get; set; }
    }
    
    [System.Serializable]
    public class SPAuthLoginCustomIdRequest : SPAuthLoginRequestBase
    {
        public string customId { get; set; }
    }
    
    public class SPAuthLoginResult : SpecterApiResultBase<SPAuthenticatedUserResponseData>
    {
        public SpecterUser User { get; private set; }
        public string AccessToken { get; private set; }
        public string EntityToken { get; private set; }

        protected override void InitSpecterObjectsInternal()
        {
            User = new SpecterUser(Response.data);
            AccessToken = Response.data.accessToken;
            EntityToken = Response.data.entityToken;
        }
    }

    public class SPAuthApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAuthApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPAuthLoginResult> LoginWithCustomId(SPAuthLoginCustomIdRequest request)
        {
            ConfigureProjectId(request);
                
            var result = await PostAsync<SPAuthLoginResult, SPAuthenticatedUserResponseData>("/v1/client/auth/login-custom", AuthType, request);
            return result;
        }
    }
}
