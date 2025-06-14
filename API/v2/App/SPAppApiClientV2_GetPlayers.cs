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
    /// Represents the filter types available for the Players endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPPlayerFilterType : SPEnum<SPPlayerFilterType>
    {
        public static readonly SPPlayerFilterType Username = new SPPlayerFilterType(0, "username", "Username");
        public static readonly SPPlayerFilterType FirstName = new SPPlayerFilterType(1, "firstName", "First Name");
        public static readonly SPPlayerFilterType LastName = new SPPlayerFilterType(2, "lastName", "Last Name");
        public static readonly SPPlayerFilterType Email = new SPPlayerFilterType(3, "email", "Email");
        public static readonly SPPlayerFilterType CustomId = new SPPlayerFilterType(4, "customId", "Custom ID");
        public static readonly SPPlayerFilterType PlayerDisplayName = new SPPlayerFilterType(5, "displayName", "Player Display Name");
        
        private SPPlayerFilterType(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a filter for the Get Players request.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPPlayerFilter
    {
        /// <summary>
        /// The filter type. Eg usage: SPPlayerFilterType.Username
        /// </summary>
        [JsonRequired]
        public SPPlayerFilterType type { get; set; }
        
        /// <summary>
        /// The value to search for with the specified filter type.
        /// </summary>
        public string value { get; set; }
        
        public SPPlayerFilter() { }
        
        public SPPlayerFilter(SPPlayerFilterType type, string value)
        {
            this.type = type;
            this.value = value;
        }
    }
    
    /// <summary>
    /// Represents a request to get players from the application.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetPlayersRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// An array of filter objects to query players. Each filter must specify a type and a corresponding value.
        /// Example: new SPPlayerFilter(SPPlayerFilterType.Username, "player123")
        /// </summary>
        public List<SPPlayerFilter> filters { get; set; }
    }

    public class SPGetPlayersResult : SpecterApiResultBase<SPGetPlayersResponse>
    {
        public List<SPPlayerProfile> Players { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Players = Response.data?.ConvertAll(x => new SPPlayerProfile(x)) ?? new List<SPPlayerProfile>();
        }
    }

    public partial class SPAppApiClientV2
    {
        public async Task<SPGetPlayersResult> GetPlayersAsync(SPGetPlayersRequest request)
        {
            var result = await PostAsync<SPGetPlayersResult, SPGetPlayersResponse>("/v2/client/app/get-players", AuthType, request);
            return result;
        }
    }
}