using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// Represents the attributes available for the Currency endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPCurrencyAttribute : SPEnum<SPCurrencyAttribute>
    {
        public static readonly SPCurrencyAttribute Meta = new SPCurrencyAttribute(0, "meta", "Meta");
        public static readonly SPCurrencyAttribute Tags = new SPCurrencyAttribute(1, "tags", "Tags");
        
        private SPCurrencyAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to fetch currencies with optional filters.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCurrenciesRequestV2 : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of currency IDs to filter results by.
        /// </summary>
        public List<string> currencyIds { get; set; }
        
        /// <summary>
        /// Filter currencies by a specific type (virtual or real). Eg usage: SPCurrencyTypeV2.Virtual
        /// </summary>
        public SPCurrencyType type { get; set; }
        
        /// <summary>
        /// A string to search for currencies by name.
        /// </summary>
        public string search { get; set; }
        
        /// <summary>
        /// An array of tags to filter the currencies by.
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Specific attributes of currencies to include in the response. Eg usage: SPCurrencyAttribute.Meta
        /// </summary>
        public List<SPCurrencyAttribute> attributes { get; set; }
    }

    public class SPGetCurrenciesResultV2 : SpecterApiResultBase<SPGetCurrenciesResponse>
    {
        public List<SPCurrency> Currencies { get; set; }
        public int TotalCount { get; set; }
        public DateTime? LastUpdate { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = Response.data?.currencies == null ? new List<SPCurrency>() : Response.data.currencies.ConvertAll(x => new SPCurrency(x));
            TotalCount = Response.data?.totalCount ?? 0;
            LastUpdate = Response.data?.lastUpdate;
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetCurrenciesResultV2> GetCurrenciesAsync(SPGetCurrenciesRequestV2 request)
        {
            var result = await PostAsync<SPGetCurrenciesResultV2, SPGetCurrenciesResponse>("/v2/client/app/get-currencies", AuthType, request);
            return result;
        }
    }
}