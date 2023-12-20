using System.Threading.Tasks;
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

    public partial class SPAuthApiClient: SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAuthApiClient(SpecterRuntimeConfig config) : base(config) {}

        private void StoreAuthContext(SPAuthLoginResult result)
        {
            if (!result.HasError && !m_Config.UseDebugCredentials)
            {
                m_Config.AccessToken = result.AccessToken;
                m_Config.EntityToken = result.EntityToken;
            }
        }
    }
}
