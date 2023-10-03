using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Tasks
{
    public partial class SPTasksApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPTasksApiClient(SpecterRuntimeConfig config) : base(config) {}
    }
}