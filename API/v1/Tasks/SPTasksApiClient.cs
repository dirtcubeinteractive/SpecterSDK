using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking;

namespace SpecterSDK.API.v1.Tasks
{
    /// <summary>
    /// The <see cref="SPTasksApiClient"/> plays a central role in enhancing player engagement by managing tasks and task groups within the Specter achievements systems.
    /// It provides methods to forcibly complete a task, fetch a player's progress for a specific task or a group of related tasks.
    /// The tasks systems are designed to foster a dynamic and interactive gaming environment through the creation and management of a myriad of tasks and objectives.
    /// Developers can leverage its features to design and implement various task types, thereby ensuring sustained player interest and progression within the game.
    /// <remarks>
    /// For more information about tasks and task groups visit the <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/engage/achievements">Specter User Manual</a>
    /// </remarks>
    /// </summary>
    public partial class SPTasksApiClient : SpecterApiClientBase
    {
        /// <summary>
        /// The Tasks API uses a user's access token for authorization.
        /// The SDK handles this internally after a user has been logged in.
        /// </summary>
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPTasksApiClient(SpecterRuntimeConfig config) : base(config) {}
    }
}