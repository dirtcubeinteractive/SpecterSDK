using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.API.v2.Players.Me
{
    /// <summary>
    /// Represents a request to update the player's profile.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateMyPlayerProfileRequest : SPApiRequestBase
    {
        /// <summary>
        /// Player's first name.
        /// </summary>
        public string firstName { get; set; }
        
        /// <summary>
        /// Player's last name.
        /// </summary>
        public string lastName { get; set; }
        
        /// <summary>
        /// Unique username of the player.
        /// </summary>
        public string username { get; set; }
        
        /// <summary>
        /// Display name for the player.
        /// </summary>
        public string displayName { get; set; }
        
        /// <summary>
        /// URL to the player's profile picture or avatar.
        /// </summary>
        public string thumbUrl { get; set; }
        
        /// <summary>
        /// Boolean indicating if the player's KYC is complete.
        /// </summary>
        public bool? isKycComplete { get; set; }
        
        /// <summary>
        /// Player's birthdate in string format.
        /// </summary>
        public string birthdate { get; set; }
        
        /// <summary>
        /// Array of tags associated with the player.
        /// </summary>
        public List<string> tags { get; set; }
        
        /// <summary>
        /// Custom parameters for processing.
        /// </summary>
        public Dictionary<string, object> customParams { get; set; }
    }

    // TODO: Should updated profile be returned?
    public class SPUpdateMyPlayerProfileResult : SpecterApiResultBase<SPUpdateMyPlayerProfileResponse>
    {
        protected override void InitSpecterObjectsInternal() { }
    }

    public partial class SPMePlayerClientV2
    {
        public async Task<SPUpdateMyPlayerProfileResult> UpdateProfileAsync(SPUpdateMyPlayerProfileRequest request)
        {
            var result = await PostAsync<SPUpdateMyPlayerProfileResult, SPUpdateMyPlayerProfileResponse>("/v2/client/player/me/update-profile", AuthType, request);
            return result;
        }
    }
}