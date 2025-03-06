using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    /// <summary>
    /// <para>
    /// The <see cref="SPRewardsApiClient"/> is intended to manage the rewarding of users/players in Specter-powered applications.
    /// It facilitates the distribution of various reward types, such as items, bundles, currencies, and progression markers, thereby boosting player engagement and satisfaction.
    /// </para>
    /// <para>
    /// It provides functionalities to grant rewards to players, supporting multitude reward types vital for recognizing player achievements and participation in events.
    /// </para>
    /// <para>
    /// Moreover, it can fetch the history of rewards granted to users, providing insights for use in your UI or analytics.
    /// With this API, developers can effectively reward user achievements and progression, encourage continued game involvement, and manage rewards distribution.
    /// </para>
    /// <remarks>
    /// For sample use cases and more information about configuring rewards and reward contents for use with the Specter API and SDK, see the Progression and Achievements sections
    /// <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual">Specter User Manual</a>
    /// </remarks>
    /// </summary>
    public partial class SPRewardsApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Rewards API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPRewardsApiClient(SpecterRuntimeConfig config) : base(config) { }
    }
}
