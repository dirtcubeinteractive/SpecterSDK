using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents the result of the Welcome API call in the SPAppApiClient class.
    /// </summary>
    public class SPWelcomeResult : SpecterApiResultBase<SPGeneralListResponseData>
    {
        public List<object> ObjectList;
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectList = Response.data;
        }
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// A simple API call to test your SDK integration. Make sure you enable logging in the Specter config, or manually log the result
        /// to ensure a successful API call.
        /// </summary>
        /// <returns>Returns a task that represents the asynchronous operation. The task result contains the welcome result as an instance of <see cref="SPWelcomeResult"/>.</returns>
        public async Task<SPWelcomeResult> Welcome()
        {
            var result = await PostAsync<SPWelcomeResult, SPGeneralListResponseData>("/v1/client/app/welcome", SPAuthType.None, null);
            return result;
        }
    }
}