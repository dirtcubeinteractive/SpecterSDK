using SpecterSDK.API.ClientAPI.Leaderboards;
using SpecterSDK.API.ClientAPI.Matches;
using SpecterSDK.API.ClientAPI.Progression;
using SpecterSDK.API.ClientAPI.Rewards;
using SpecterSDK.API.ClientAPI.Stores;
using SpecterSDK.API.ClientAPI.Tasks;
using SpecterSDK.API.ClientAPI.User;
using SpecterSDK.API.ClientAPI.v1.App;
using SpecterSDK.API.ClientAPI.v1.Authentication;
using SpecterSDK.API.ClientAPI.v1.Competitions;
using SpecterSDK.API.ClientAPI.v1.Events;
using SpecterSDK.API.ClientAPI.v1.Inventory;
using SpecterSDK.API.ClientAPI.Wallet;

namespace SpecterSDK.Shared.Versions
{
    public class SpecterApiV1 : SpecterApiBase
    {
        public SPAppApiClient App { get; private set; }

        /// <summary>
        /// Provides methods to authenticate users, manage sessions, and handle user credentials.
        /// </summary>
        public SPAuthApiClient Auth { get; private set; }

        /// <summary>
        /// Provides methods for interacting with the Competitions API.
        /// </summary>
        public SPCompetitionsApiClient Competitions { get; private set; }

        /// <summary>
        /// Provides access to the Specter custom events API.
        /// </summary>
        public SPEventsApiClient Events { get; private set; }

        /// <summary>
        /// Provides methods to retrieve, update and manage a user's inventory in your game.
        /// </summary>
        public SPInventoryApiClient Inventory { get; private set; }

        /// <summary>
        /// Provides methods to retrieve, update and manage leaderboards in your game.
        /// </summary>
        public SPLeaderboardsApiClient Leaderboards { get; private set; }

        /// <summary>
        /// Provides methods to manage match sessions when a user plays your game.
        /// </summary>
        public SPMatchesApiClient Matches { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage info about a user's progress, update their progress, etc.
        /// </summary>
        public SPProgressionApiClient Progression { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage info about a user's rewards, grant rewards, etc.
        /// </summary>
        public SPRewardsApiClient Rewards { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage info about stores created for your app, store categories and other stores related APIs.
        /// </summary>
        public SPStoreApiClient Stores { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage tasks, grant rewards, and other task related data.
        /// </summary>
        public SPTasksApiClient Tasks { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage user profiles, attributes, and other user-related data.
        /// </summary>
        public SPUserApiClient User { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage user currencies (eg: retrieving balance, updating balance, etc.).
        /// </summary>
        public SPWalletApiClient Wallet { get; private set; }

        public override void Initialize(SpecterRuntimeConfig config)
        {
            App = new SPAppApiClient(config);
            Auth = new SPAuthApiClient(config);
            Competitions = new SPCompetitionsApiClient(config);
            Events = new SPEventsApiClient(config);
            Inventory = new SPInventoryApiClient(config);
            Leaderboards = new SPLeaderboardsApiClient(config);
            Matches = new SPMatchesApiClient(config);
            Progression = new SPProgressionApiClient(config);
            Rewards = new SPRewardsApiClient(config);
            Stores = new SPStoreApiClient(config);
            Tasks = new SPTasksApiClient(config);
            User = new SPUserApiClient(config);
            Wallet = new SPWalletApiClient(config);
        }

        public override void Dispose()
        {
            App = null;
            Auth = null;
            Competitions = null;
            Events = null;
            Inventory = null;
            Leaderboards = null;
            Matches = null;
            Progression = null;
            Rewards = null;
            Stores = null;
            Tasks = null;
            User = null;
            Wallet = null;
        }
    }
}