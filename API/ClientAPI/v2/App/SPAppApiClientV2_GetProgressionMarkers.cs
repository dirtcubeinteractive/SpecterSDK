using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.ClientAPI.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Progression Marker endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPMarkerAttribute : SPEnum<SPMarkerAttribute>
    {
        public static readonly SPMarkerAttribute Meta = new SPMarkerAttribute(0, "meta", "Meta");
        public static readonly SPMarkerAttribute Tags = new SPMarkerAttribute(1, "tags", "Tags");
        public static readonly SPMarkerAttribute ProgressionSystems = new SPMarkerAttribute(2, "progressionSystems", "Progression Systems");
        
        private SPMarkerAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch progression markers from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetProgressionMarkersRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of progression marker IDs to fetch specific markers.
        /// </summary>
        public List<string> progressionMarkerId { get; set; }
        
        /// <summary>
        /// A string to search for progression markers by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// A list of tags to filter the progression marker data.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of progression markers to include in the response. Eg usage: SPMarkerAttribute.ProgressionSystems
        /// </summary>
        public List<SPMarkerAttribute> attributes { get; set; }
    }

    public class SPGetProgressionMarkersResultV2 : SpecterApiResultBase<SPGetProgressionMarkersResponse>
    {
        public List<SPProgressionMarker> Markers { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Markers = Response.data?.markers == null ? new List<SPProgressionMarker>() : Response.data.markers.ConvertAll(x => new SPProgressionMarker(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetProgressionMarkersResultV2> GetProgressionMarkersAsync(SPGetProgressionMarkersRequestV2 request)
        {
            var result = await PostAsync<SPGetProgressionMarkersResultV2, SPGetProgressionMarkersResponse>("/v2/client/app/get-markers", AuthType, request);
            return result;
        }
    }
}