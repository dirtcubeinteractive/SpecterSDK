using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Others
{
    /// <summary>
    /// Represents a request to get profile information for another player.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetOtherPlayerProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public string userId { get; set; }
    }
    
    public class SPGetOtherPlayerProfileResult : SpecterApiResultBase<SPGetOtherPlayerProfileResponse>
    {
        public SPPlayerProfile Profile { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Profile = new SPPlayerProfile(Response.data.user);
        }
    }

    public partial class SPOtherPlayerClientV2
    {
        public async Task<SPGetOtherPlayerProfileResult> GetPlayerProfileAsync(SPGetOtherPlayerProfileRequest request)
        {
            var result = await PostAsync<SPGetOtherPlayerProfileResult, SPGetOtherPlayerProfileResponse>("/v2/client/player/get-profile", AuthType, request);
            return result;
        }
    }
}