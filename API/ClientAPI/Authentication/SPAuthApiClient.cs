using System;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Authentication
{
    [System.Serializable]
    public abstract class SPAuthLoginRequestBase : SPApiRequestBase, IProjectConfigurable
    {
        public string projectId { get; set; }
        public bool createAccount { get; set; }
    }

    public class SPAuthLoginResult : SpecterApiResultBase<SPUserAuthResponseData>
    {
        public SpecterUser User { get; private set; }
        public string AccessToken { get; private set; }
        public string EntityToken { get; private set; }
        public bool CreatedAccount { get; private set; }

        protected override void InitSpecterObjectsInternal()
        {
            if (Response.data == null)
                return;
            
            User = new SpecterUser(Response.data.user);
            AccessToken = Response.data.accessToken;
            EntityToken = Response.data.entityToken;
            CreatedAccount = Response.data.createdAccount;
        }
    }

    public partial class SPAuthApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAuthApiClient(SpecterRuntimeConfig config) : base(config) {}

        private void StoreAuthContext(SPAuthLoginResult result)
        {
            if (!result.HasError && !m_Config.UseDebugCredentials)
            {
                SpecterRuntimeConfig.AuthCredentials ??= new SPAuthContext();
                m_Config.AccessToken = result.AccessToken;
                m_Config.EntityToken = result.EntityToken;
            }
        }
    }
}
