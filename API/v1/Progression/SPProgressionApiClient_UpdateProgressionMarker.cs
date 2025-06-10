using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Interfaces;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Progression
{
    /// <summary>
    /// Represents a request to update the progression marker amount for a user in the Specter SDK.
    /// </summary>
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateProgressionMarkerRequest : SPApiRequestBase, ISpecterEventConfigurable
    {   
        /// <summary>
        /// The operation for the update. See <see cref="SPOperations"/> for possible values.
        /// </summary>
        public SPOperations operation { get; set; }
        
        /// <summary>
        /// The amount to update the progression marker by.
        /// </summary>
        public int amount { get; set; }
        
        /// <summary>
        /// The dashboard specified ID of the progression marker.
        /// </summary>
        public string progressionMarkerId { get; set; }
        
        /// <summary>
        /// Dictionary of optional Specter params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> specterParams { get; set; }
            
        /// <summary>
        /// Dictionary of optional custom params to be sent with the API call
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    /// <summary>
    /// Represents the result of updating a progression marker.
    /// </summary>
    public class SPUpdateProgressionMarkerResult : SpecterApiResultBase<SPUserProgressResponseData>
    {
        // The updated progression marker details and their associated progression systems.
        public SpecterUserProgress UpdatedProgress;
        
        protected override void InitSpecterObjectsInternal()
        {
            UpdatedProgress = new SpecterUserProgress(Response.data);
        }
    }

    public partial class SPProgressionApiClient
    {
        /// <summary>
        /// Update the progression marker amounts of a user asynchronously.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Updating the amount of a progression marker will also update the user's progress within the marker's associated
        /// progression systems. That means, that if the marker is used in multiple progression systems, updating the marker
        /// amount could update the user's level in the associated systems depending on the threshold values configured on the
        /// dashboard.
        /// </para>
        /// <para>
        /// For full information about the update progression marker endpoint, see the Progression section of the <a href="https://doc.specterapp.xyz">Specter API Docs</a>.
        /// </para>
        /// </remarks>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPUpdateProgressionMarkerRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPUpdateProgressionMarkerResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPUpdateProgressionMarkerResult> UpdateProgressionMarkerAsync(SPUpdateProgressionMarkerRequest request)
        {
            var task = await PostAsync<SPUpdateProgressionMarkerResult, SPUserProgressResponseData>("/v1/client/progression/update-marker", AuthType, request);
            return task;
        }
    }
}