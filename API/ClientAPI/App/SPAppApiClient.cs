using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI.App
{
    /// <summary>
    /// SPAppApiClient class is responsible for making API calls related to app level data.
    /// App level data is data configured on the Specter dashboard for use in your game, eg: tasks,
    /// currencies, items, etc.
    /// </summary>
    public partial class SPAppApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// App API routes usually require a user's access token
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPAppApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}