using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Networking.Models;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents the attributes available for the Player Profile endpoint.
    /// </summary>
    [Serializable]
    public sealed class SPPlayerProfileAttribute : SPEnum<SPPlayerProfileAttribute>
    {
        public static readonly SPPlayerProfileAttribute LinkedAccounts = new SPPlayerProfileAttribute(0, "linkedAccounts", "Linked Accounts");
        public static readonly SPPlayerProfileAttribute EquippedItems = new SPPlayerProfileAttribute(1, "equippedItems", "Equipped Items");
        
        private SPPlayerProfileAttribute(int id, string name, string displayName) : base(id, name, displayName) { }
    }
    
    /// <summary>
    /// Represents a request to get player profile information.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetMyPlayerProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// Specific attributes to include in the response.
        /// </summary>
        public List<SPPlayerProfileAttribute> attributes { get; set; }
    }

    public class SPGetMyPlayerProfileResult : SpecterApiResultBase<SPGetMyPlayerProfileResponse>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPGetMyPlayerProfileResult> GetPlayerProfileAsync(SPGetMyInventoryCollectionsRequest request)
        {
            var result = await PostAsync<SPGetMyPlayerProfileResult, SPGetMyPlayerProfileResponse>("/v2/client/player/me/get-profile", AuthType, request);
            return result;
        }
    }
}