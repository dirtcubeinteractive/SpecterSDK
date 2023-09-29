using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI
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
