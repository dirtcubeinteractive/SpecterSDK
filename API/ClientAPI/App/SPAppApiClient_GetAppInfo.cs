using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents the request to get yor app's information from Specter.
    /// </summary>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetAppInfoRequest : SPApiRequestBase
    {
        /// <summary>
        /// List of strings representing specific attributes of your app that you want
        /// to retrieve.
        /// For information about possible attributes, see the Get App Info route in the App section
        /// of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>.
        /// </summary>
        public List<string> attributes { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetAppInfoAsync method in the SPAppApiClient class.
    /// </summary>
    public class SPGetAppInfoResult : SpecterApiResultBase<SPAppInfoResponseData>
    {
        public SpecterApp App { get; private set; }

        protected override void InitSpecterObjectsInternal()
        {
            App = new SpecterApp(Response.data);
        }
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// Retrieves the information about the app from the Specter API asynchronously.
        /// The information retrieved corresponds to the App Information subsection in the App Settings module on
        /// the Specter dashboard. See <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/app/app-settings/app-configuration">Specter User Manual</a> for more details on app information.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetAppInfoRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetAppInfoResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetAppInfoResult> GetAppInfoAsync(SPGetAppInfoRequest request)
        {
            var result = await PostAsync<SPGetAppInfoResult, SPAppInfoResponseData>("/v1/client/app/get-info", AuthType, request);
            return result;
        }
    }
}