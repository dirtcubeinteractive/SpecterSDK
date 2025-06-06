using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// Represents a request to get bundles from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetBundlesRequest class represents a request to get bundles from the Specter App API.
    /// It can be used to specify the filter criteria for the bundles to be returned.
    /// </para>
    /// <para>
    /// This request can be sent to the GetBundlesAsync method in the SPAppApiClient class to retrieve the bundles from the API.
    /// </para>
    /// </remarks>
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetBundlesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of bundle IDs used as filter criteria for retrieving bundles from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The bundleIds property is used in the SPGetBundlesRequest class to specify the specific bundle IDs for filtering the retrieved bundles from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique bundle ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> bundleIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }

        /// <summary>
        /// Additional attributes of the retrieved bundles that you wish to receive (eg: createdAt, updatedAt etc.)
        /// </summary>
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// Specify this value if you wish to retrieve only bundles that have lock conditions
        /// (eg: locked by progression systems, locked by item, etc. see <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/economy/bundles/bundle-configuration#access-and-eligibility">Access & Eligibility</a> for more info)
        /// </summary>
        public bool? isLocked { get; set; }
        
        /// <summary>
        /// Filter to search for and retrieve bundles by their name
        /// Multiple Bundles can be fetched by setting a name substring
        /// <example>
        /// Bundles named Tier 1 Bundle and Tier 2 Bundle can be fetched by searching for 'tier' or 'Tier' </example>
        /// </summary>
        public string search { get; set; }
    }

    /// <summary>
    /// Represents the result of the GetBundlesAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetBundlesResult : SpecterApiResultBase<SPGetBundlesResponseData>
    {
        // List of all bundles retrieved
        public List<SpecterBundle> Bundles;
        
        // Total count of all bundles created on your app dashboard
        public int TotalBundleCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            Bundles = new List<SpecterBundle>();
            foreach (var itemData in Response.data.bundles)
            {
                Bundles.Add(new SpecterBundle(itemData));
            }
            
            TotalBundleCount = Response.data.totalCount;
        }
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of bundles asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetBundlesRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetBundlesResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetBundlesResult> GetBundlesAsync(SPGetBundlesRequest request)
        {
            var result = await PostAsync<SPGetBundlesResult, SPGetBundlesResponseData>("/v1/client/app/get-bundles", AuthType, request);
            return result;
        }
    }

}
