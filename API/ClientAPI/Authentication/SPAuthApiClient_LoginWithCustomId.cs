using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Authentication
{
    [System.Serializable]
    public class SPAuthLoginCustomIdRequest : SPAuthLoginRequestBase
    {
        public string customId { get; set; }
    }
    
    public partial class SPAuthApiClient
    {
        public async Task<SPAuthLoginResult> LoginWithCustomIdAsync(SPAuthLoginCustomIdRequest request)
        {
            var result = await PostAsync<SPAuthLoginResult, SPAuthenticatedUserResponseData>("/v1/client/auth/login-custom", AuthType, request);
            StoreAuthContext(result);
            
            return result;
        }
    }
}