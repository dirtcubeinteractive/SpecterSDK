using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Stores endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPStoreAttribute : SPEnum<SPStoreAttribute>
    {
        public static readonly SPStoreAttribute UnlockConditions = new SPStoreAttribute(0, "unlockConditions", "Unlock Conditions");
        public static readonly SPStoreAttribute Meta = new SPStoreAttribute(1, "meta", "Meta");
        public static readonly SPStoreAttribute Tags = new SPStoreAttribute(2, "tags", "Tags");
        
        private SPStoreAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to retrieve details about in-game stores.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetStoresRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// Array of store IDs to look up (e.g., 'main_store', 'holiday_shop').
        /// </summary>
        public List<string> storeIds { get; set; }
        
        /// <summary>
        /// Keyword-based search for store names.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// Filter or categorize stores based on tags (e.g. 'featured', 'seasonal').
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of stores to include in the response. E.g. usage: SPStoreAttribute.UnlockConditions
        /// </summary>
        public List<SPStoreAttribute> attributes { get; set; }
    }

    public class SPGetStoresResultV2 : SpecterApiResultBase<SPGetStoresResponse>
    {
        public List<SPStore> Stores { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Stores = Response.data == null ? new List<SPStore>() : Response.data.stores.ConvertAll(x => new SPStore(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetStoresResultV2> GetStoresAsync(SPGetStoresRequestV2 request)
        {
            var result = await PostAsync<SPGetStoresResultV2, SPGetStoresResponse>("/v2/client/app/get-stores", AuthType, request);
            return result;
        }
    }
}