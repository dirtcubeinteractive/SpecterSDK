using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v2.App
{
    /// <summary>
    /// SPAppApiClient class is responsible for making API calls related to app level data.
    /// App level data is data configured on the Specter dashboard for use in your game, eg: tasks,
    /// currencies, items, etc.
    /// </summary>
    public partial class SPAppApiClientV2 : SpecterApiClientBase
    {
        /// <summary>
        /// The App API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.None;

        public SPAppApiClientV2(SpecterRuntimeConfig config) : base(config)
        {
        }
    }
}