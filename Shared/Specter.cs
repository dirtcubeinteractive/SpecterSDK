using System;
using System.Collections.Generic;
using System.Linq;
using SpecterSDK.API;
using SpecterSDK.API.ClientAPI.App;
using SpecterSDK.API.ClientAPI.Authentication;
using SpecterSDK.API.ClientAPI.Events;
using SpecterSDK.API.ClientAPI.Inventory;
using SpecterSDK.API.ClientAPI.Leaderboards;
using SpecterSDK.API.ClientAPI.Matches;
using SpecterSDK.API.ClientAPI.Progression;
using SpecterSDK.API.ClientAPI.Rewards;
using SpecterSDK.API.ClientAPI.Stores;
using SpecterSDK.API.ClientAPI.Tasks;
using SpecterSDK.API.ClientAPI.User;
using SpecterSDK.API.ClientAPI.Wallet;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Attributes;
using UnityEngine;

namespace SpecterSDK
{
    /// <summary>
    /// Main entry point for the Specter SDK.
    /// Provides properties and methods for initializing and accessing various APIs.
    /// </summary>
    public static class Specter
    {
        #region Path constants & props

        /// <summary>
        /// The root directory name for the SDK. This is where all SDK-related assets and resources are to be stored.
        /// </summary>
        public const string SDK_DIRNAME = "SpecterSDK";
        
        /// <summary>
        /// Directory where shared resources, common to multiple parts of the SDK, are stored.
        /// </summary>
        public const string SHARED_DATA_DIRNAME = "SpecterSharedResources";
        
        /// <summary>
        /// The main configuration file for the SDK. This contains settings and preferences & data for the SDK to operate.
        /// </summary>
        public const string CONFIG_FILENAME = "SpecterConfigData";

        /// <summary>
        /// Directory where config scriptable object is stored.
        /// </summary>
        public static string ConfigDataResourcePath => $"{SHARED_DATA_DIRNAME}/{CONFIG_FILENAME}";
        
        #endregion
        
        /// <summary>
        /// Represents initialization options for the Specter SDK.
        /// </summary>
        public class InitOptions
        {
            public SPEnvironment Environment { get; set; }
            public string ProjectId { get; set; }
            public SPAuthContext AuthContext { get; set; }
        }
        
        /// <summary>
        /// Contains the runtime settings and configurations for the Specter SDK.
        /// This is built from the scriptable object Specter Config Data but can be modified at runtime if needed.
        /// </summary>
        public static SpecterRuntimeConfig Config;
        
        public static SPAppApiClient App { get; private set; }
        
        /// <summary>
        /// Provides methods to authenticate users, manage sessions, and handle user credentials.
        /// </summary>
        public static SPAuthApiClient Auth { get; private set; }

        /// <summary>
        /// Provides access to the Specter custom events API.
        /// </summary>
        public static SPEventsApiClient Events { get; private set; }

        /// <summary>
        /// Provides methods to retrieve, update and manage a user's inventory in your game.
        /// </summary>
        public static SPInventoryApiClient Inventory { get; private set; }

        /// <summary>
        /// Provides methods to retrieve, update and manage leaderboards in your game.
        /// </summary>
        public static SPLeaderboardsApiClient Leaderboards { get; private set; }
        
        /// <summary>
        /// Provides methods to manage match sessions when a user plays your game.
        /// </summary>
        public static SPMatchesApiClient Matches { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage info about a user's progress, update their progress, etc.
        /// </summary>
        public static SPProgressionApiClient Progression { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage info about a user's rewards, grant rewards, etc.
        /// </summary>
        public static SPRewardsApiClient Rewards { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage info about stores created for your app, store categories and other stores related APIs.
        /// </summary>
        public static SPStoreApiClient Stores { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage tasks, grant rewards, and other task related data.
        /// </summary>
        public static SPTasksApiClient Tasks { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage user profiles, attributes, and other user-related data.
        /// </summary>
        public static SPUserApiClient User { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage user currencies (eg: retrieving balance, updating balance, etc.).
        /// </summary>
        public static SPWalletApiClient Wallet { get; private set; }
        
        /// <summary>
        /// Represents a dictionary of custom Specter API clients. Custom API clients can be created by subclassing
        /// <see cref="SpecterApiClientBase"/>. You must add the <see cref="SpecterCustomApiClientAttribute"/> attribute
        /// to the class in order for the SDK to find and initialize it, else you can initialize it manually.
        /// </summary>
        private static Dictionary<Type, SpecterApiClientBase> CustomClients;
        
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Loads the configuration asset for the Specter SDK from the Unity Resources folder.
        /// </summary>
        /// <returns>The runtime configuration built from the config data.</returns>
        public static SpecterRuntimeConfig LoadConfig()
        {
            var configData = Resources.Load<SpecterConfigData>(ConfigDataResourcePath);
            if (configData != null)
            {
                Config = new SpecterRuntimeConfig(configData);
                return Config;
            }

            Debug.LogError("No Specter config data found. Specter will need to be initialized manually by calling Specter.Initialize(options)");
            return null;
        }

        /// <summary>
        /// Automatically initializes the Specter SDK when the game starts, if the AutoInit setting is enabled in the configuration scriptable object.
        /// Should not be called manually.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void AutoInitialize()
        {
            if (Config != null)
            {
                Debug.LogError("Specter Config already initialized. Cannot Auto Init");
                return;
            }

            var configData = Resources.Load<SpecterConfigData>(ConfigDataResourcePath);
            if (configData == null)
            {
                Debug.LogWarning("No Specter config data found. Specter will need to be initialized manually by calling Specter.Initialize()");
                return;
            }

            if (!configData.AutoInit)
                return;

            Initialize(configData);
        }

        /// <summary>
        /// Manually initialize Specter with SpecterConfigData asset
        /// </summary>
        public static void InitializeWithConfig()
        {
            Config = LoadConfig();
            if (Config == null)
                return;
            
            InitializeApi();
        }
        
        /// <summary>
        /// Initializes the Specter SDK with the provided options. This sets up the API clients and other necessary components for the SDK to function.
        /// Use this for completely controlled manual initialization
        /// </summary>
        /// <param name="options">The set of options to use for initialization. If not provided, defaults will be used.</param>
        public static void Initialize(InitOptions options)
        {
            options ??= new InitOptions() { Environment = SPEnvironment.Development, ProjectId = ""};
            Config = new SpecterRuntimeConfig(options.Environment, options.ProjectId);

            if (options.AuthContext != null)
            {
                Config.AccessToken = options.AuthContext.AccessToken;
                Config.EntityToken = options.AuthContext.EntityToken;
            }
            
            InitializeApi();
        }

        /// <summary>
        /// Initializes the application using the provided Specter Configuration data.
        /// This is for manual init using the config scriptable object.
        /// </summary>
        /// <param name="configData">An instance of SpecterConfigData that represents the configuration data for Specter SDK.</param>
        public static void Initialize(SpecterConfigData configData)
        {
            Config = new SpecterRuntimeConfig(configData);
            InitializeApi();
        }

        /// <summary>
        /// Initializes all the Api clients for the SDK to function
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the config has not been loaded before attempting to initialize the SDK.
        /// </exception>
        private static void InitializeApi()
        {
            if (Config == null)
            {
                throw new InvalidOperationException(
                    "Specter Runtime Config must be initialized before initializing the SDK Http clients. " +
                    "Call Specter.Initialize or Specter.InitializeWithConfig or enable Auto Init " +
                    "in SpecterConfigData Scriptable Object");
            }

            App = new SPAppApiClient(Config);
            Auth = new SPAuthApiClient(Config);
            Events = new SPEventsApiClient(Config);
            Inventory = new SPInventoryApiClient(Config);
            Leaderboards = new SPLeaderboardsApiClient(Config);
            Matches = new SPMatchesApiClient(Config);
            Progression = new SPProgressionApiClient(Config);
            Rewards = new SPRewardsApiClient(Config);
            Stores = new SPStoreApiClient(Config);
            Tasks = new SPTasksApiClient(Config);
            User = new SPUserApiClient(Config);
            Wallet = new SPWalletApiClient(Config);
            
            LoadCustomClients();
            
            IsInitialized = true;
        }

        private static void LoadCustomClients()
        {
            CustomClients = new Dictionary<Type, SpecterApiClientBase>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t =>
                    t.BaseType == typeof(SpecterApiClientBase)
                    && !t.IsAbstract
                    && t.GetCustomAttributes(typeof(SpecterCustomApiClientAttribute), false).Length == 1);
                foreach (var type in types)
                {
                    var client = Activator.CreateInstance(type, Config) as SpecterApiClientBase;
                    CustomClients.Add(type, client);
                }
            }
        }

        /// <summary>
        /// Retrieves the custom client of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the custom client to retrieve.</typeparam>
        /// <returns>The custom client of type T.</returns>
        /// <exception cref="ArgumentException">Thrown if no custom client of the specified type is found.</exception>
        /// <exception cref="InvalidCastException">Thrown if the retrieved custom client cannot be converted to type T.</exception>
        /// <remarks>
        /// The custom client must implement the SpecterCustomApiClientAttribute to be loaded by the SDK.
        /// </remarks>
        public static T GetCustomClient<T>() where T : SpecterApiClientBase
        {
            if (!CustomClients.TryGetValue(typeof(T), out var client))
                throw new ArgumentException(
                    $"No custom client of type {typeof(T).Name} found. Please ensure that your custom API client implements the {nameof(SpecterCustomApiClientAttribute)} in order to be loaded by the SDK");
            
            if (client is T customClient)
                return customClient;
                
            throw new InvalidCastException($"{client.GetType().AssemblyQualifiedName} cannot be converted to type {typeof(T).AssemblyQualifiedName}");

        }

        /// <summary>
        /// Disposes of all resources used by the SpecterSDK.
        /// </summary>
        /// <remarks>
        /// Use if you need to reset the Specter SDK and manually re-initialize.
        /// </remarks>
        public static void Dispose()
        {
            App = null;
            Auth = null;
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
            
            CustomClients.Clear();
            
            Config = null;
            IsInitialized = false;
        }
    }
}
