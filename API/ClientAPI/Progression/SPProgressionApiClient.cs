using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.Progression
{
    /// <summary>
    /// The SPProgressionApiClient provides a set of endpoints for developers to manage player progressions (eg: XP progressions) using Specter.
    /// This API facilitates dynamic progression systems by allowing to update progression markers and retrieve player's current progression status. 
    /// Use of this API can enhance user engagement and improve retention by providing rewarding and personalized gaming experiences.
    /// <remarks>
    /// For more information about configuring progression markers and progression systems for use with the Specter API and SDK, see the
    /// <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/progression">Specter User Manual</a>
    /// </remarks>
    /// </summary>
    public partial class SPProgressionApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Progression API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPProgressionApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}