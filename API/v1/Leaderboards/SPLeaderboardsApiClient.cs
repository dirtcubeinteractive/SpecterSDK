using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v1.Leaderboards
{
    /// <summary>
    /// Represents the client API for Leaderboards in the Specter SDK.
    /// Leaderboards API is a cornerstone of any competitive digital environment and provides the functionalities to manage and display competitive rankings among players.
    /// For more details, refer to the Leaderboards section in the <a href="https://doc.specterapp.xyz">Specter API Documentation</a>.
    /// </summary>
    public partial class SPLeaderboardsApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Leaderboards API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPLeaderboardsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}