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
    /// Represents the attributes available for the Bundles endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPBundleAttribute : SPEnum<SPBundleAttribute>
    {
        public static readonly SPBundleAttribute Properties = new SPBundleAttribute(0, "properties", "Properties");
        public static readonly SPBundleAttribute Prices = new SPBundleAttribute(1, "prices", "Prices");
        public static readonly SPBundleAttribute Contents = new SPBundleAttribute(2, "contents", "Contents");
        public static readonly SPBundleAttribute UnlockConditions = new SPBundleAttribute(3, "unlockConditions", "Unlock Conditions");
        public static readonly SPBundleAttribute Meta = new SPBundleAttribute(4, "meta", "Meta");
        public static readonly SPBundleAttribute Tags = new SPBundleAttribute(5, "tags", "Tags");
        
        private SPBundleAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to retrieve bundles from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetBundlesRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of specific bundle IDs to look up (e.g., 'starter_pack', 'holiday_bundle').
        /// </summary>
        public List<string> bundleIds { get; set; }
        
        /// <summary>
        /// Filter by locked (true) or unlocked (false) status. A locked bundle may require special conditions, like level requirements.
        /// </summary>
        public bool? isLocked { get; set; }
        
        /// <summary>
        /// Search keyword to filter bundles by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Array of tags to narrow down or categorize the bundles.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of bundles to include in the response. Eg usage: SPBundleAttribute.Contents
        /// </summary>
        public List<SPBundleAttribute> attributes { get; set; }
    }

    public class SPGetBundlesResultV2 : SpecterApiResultBase<SPGetBundlesResponse>
    {
        public List<SPBundle> Bundles { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Bundles = Response.data?.bundles == null ? new List<SPBundle>() : Response.data.bundles.ConvertAll(x => new SPBundle(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetBundlesResultV2> GetBundlesAsync(SPGetBundlesRequestV2 request)
        {
            var result = await PostAsync<SPGetBundlesResultV2, SPGetBundlesResponse>("/v2/client/app/get-bundles", AuthType, request);
            return result;
        }
    }
}