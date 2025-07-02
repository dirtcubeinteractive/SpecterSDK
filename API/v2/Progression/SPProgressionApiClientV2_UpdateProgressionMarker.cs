using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Progression
{
    /// <summary>
    /// Represents a request to update a progression marker.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateProgressionMarkerRequest : SPApiRequestBase
    {
        /// <summary>
        /// Identifier for the progression marker to update.
        /// </summary>
        public string progressionMarkerId { get; set; }
        
        /// <summary>
        /// The amount to adjust the progression marker by (always use a positive amount, subtractions are handled by the operation field)
        /// </summary>
        public long amount { get; set; }
        
        /// <summary>
        /// Operation to apply to the progression marker (add or subtract).
        /// </summary>
        public SPOperations operation { get; set; }
        
        /// <summary>
        /// Custom parameters for further customization.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPUpdateProgressionMarkerResult : SpecterApiResultBase<SPUpdateProgressionMarkerResponse>
    {
        public SPMarkerProgress UpdatedMarkerProgress { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            UpdatedMarkerProgress = new SPMarkerProgress(Response.data);
        }
    }

    public partial class SPProgressionApiClientV2
    {
        public async Task<SPUpdateProgressionMarkerResult> UpdateProgressionMarkerAsync(SPUpdateProgressionMarkerRequest request)
        {
            var result = await PostAsync<SPUpdateProgressionMarkerResult, SPUpdateProgressionMarkerResponse>("/v2/client/progression/update-marker", AuthType, request);
            return result;
        }
    }
}