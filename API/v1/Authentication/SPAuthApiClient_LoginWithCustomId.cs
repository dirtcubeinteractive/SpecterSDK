using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;

namespace SpecterSDK.API.v1.Authentication
{
    /// <summary>
    /// Represents a request to login with a custom ID.
    /// <remarks>
    /// A custom ID is defined by you. Login with custom Id assumes that you have
    /// already authenticated a user on your end and Specter simply generates credentials
    /// like accessToken for the user without any additional validation.
    /// </remarks>
    /// <example>
    /// If your app uses an authentication mechanism like phone number and one time password, you would set
    /// the user's phone number string as the user's custom ID.
    /// </example>
    /// </summary>
    [System.Serializable]
    public class SPAuthLoginCustomIdRequest : SPAuthLoginRequestBase
    {
        public string customId { get; set; }
    }
    
    public partial class SPAuthApiClient
    {
        public async Task<SPAuthLoginResult> LoginWithCustomIdAsync(SPAuthLoginCustomIdRequest request)
        {
            var result = await PostAsync<SPAuthLoginResult, SPUserAuthResponseData>("/v1/client/auth/login-custom", AuthType, request);
            StoreAuthContext(result);
            
            return result;
        }
    }
}