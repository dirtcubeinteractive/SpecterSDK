using System;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.v2.Matches
{
    /// <summary>
    /// Represents user information for a match session participant.
    /// </summary>
    [Serializable]
    public class SPMatchUserInfoV2
    {
        /// <summary>
        /// Unique identifier for the user participating in the match session.
        /// </summary>
        public string id { get; set; }
    }
    
    /// <summary>
    /// Represents user information with outcome data for ending a match session.
    /// </summary>
    [Serializable]
    public class SPEndMatchUserInfoV2 : SPMatchUserInfoV2
    {
        /// <summary>
        /// Outcome status or score associated with the user at the end of the session.
        /// </summary>
        public double outcome { get; set; }
    }
    
    public partial class SPMatchesApiClientV2 : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPMatchesApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}