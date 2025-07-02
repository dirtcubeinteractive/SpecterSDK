using SpecterSDK.Shared;
using SpecterSDK.Shared.Http;

namespace SpecterSDK.API.v1.Events
{
    /// <summary>
    /// <para>
    /// The SPEventsApiClient class provides methods to interact with the Specter Events API.
    /// The Events API within the Specter platform is a powerful tool tailored for tracking custom events,
    /// providing insights into player behaviors and responding to player actions effectively.
    /// </para>
    /// <para>
    /// Use of the Events API allows for active engagement with the player base through a sophisticated
    /// event-driven approach, which is a key part of understanding player behavior and dynamically enhancing 
    /// the gaming experience.
    /// </para>
    /// <para>
    /// See the Events section in the <a href="https://doc.specterapp.xyz">Specter API Documentation</a> for more info
    /// about the events API and refer to <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/build/events">Events section</a>
    /// in the Specter User Manual for information about configuring custom events.
    /// </para>
    /// </summary>
    public partial class SPEventsApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Events API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;

        public SPEventsApiClient(SpecterRuntimeConfig config) : base(config) {}
    }
}
