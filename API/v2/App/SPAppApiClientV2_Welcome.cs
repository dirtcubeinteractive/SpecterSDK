using System.Threading.Tasks;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.App
{
    public partial class SPAppApiClientV2
    {
        /// <summary>
        /// A simple API call to test your SDK integration. Make sure you enable logging in the Specter config, or manually log the result
        /// to ensure a successful API call.
        /// </summary>
        /// <returns>Returns a task that represents the asynchronous operation. The task result contains the welcome result as an instance of <see cref="SPWelcomeResult"/>.</returns>
        public async Task<SPGeneralListResult> Welcome()
        {
            var result = await PostAsync<SPGeneralListResult, SPGeneralListResponseData>("/v2/client/app/welcome", AuthType, null);
            return result;
        }
    }
}