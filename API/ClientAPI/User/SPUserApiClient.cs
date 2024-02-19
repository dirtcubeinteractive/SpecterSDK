using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.User
{
    /// <summary>
    /// The SPUserApiClient class is part of the Specter User API. It facilitates dynamic management of key
    /// user data for a logged in user in your game. The user API endpoints allow developers to read and update users'
    /// profiles from within their app or game, it allows access to manage custom data related to each user, and search for other users
    /// from their client code.
    /// </summary>
    public partial class SPUserApiClient: SpecterApiClientBase
    {
        /// <summary>
        /// The User API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPUserApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}