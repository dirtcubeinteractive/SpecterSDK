using System;
using SpecterSDK.API.ClientAPI;
using SpecterSDK.API.ClientAPI.App;
using SpecterSDK.API.ClientAPI.Authentication;
using SpecterSDK.API.ClientAPI.Events;
using SpecterSDK.API.ClientAPI.Inventory;
using SpecterSDK.API.ClientAPI.Matches;
using SpecterSDK.API.ClientAPI.Progression;
using SpecterSDK.API.ClientAPI.Rewards;
using SpecterSDK.API.ClientAPI.Stores;
using SpecterSDK.API.ClientAPI.Tasks;
using SpecterSDK.API.ClientAPI.User;
using SpecterSDK.API.ClientAPI.Wallet;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;
using UnityEditor;
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
        /// The root directory name for the SDK. This is where all SDK-related assets and resources are stored.
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
        
        public static SPEventsApiClient Events { get; private set; }
        
        public static SPInventoryApiClient Inventory { get; private set; }
        
        public static SPMatchesApiClient Matches { get; private set; }
        
        public static SPProgressionApiClient Progression { get; private set; }
        
        public static SPRewardsApiClient Rewards { get; private set; }
        
        public static SPStoreApiClient Stores { get; private set; }

        /// <summary>
        /// Provides methods to retrieve and manage tasks, grant rewards, and other task related data.
        /// </summary>
        public static SPTasksApiClient Tasks { get; private set; }
        
        /// <summary>
        /// Provides methods to retrieve and manage user profiles, attributes, and other user-related data.
        /// </summary>
        public static SPUserApiClient User { get; private set; }
        
        public static SPWalletApiClient Wallet { get; private set; }

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
        /// Automatically initializes the Specter SDK when the game starts, if the AutoInit setting is enabled in the configuration data.
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

            var options = new InitOptions() { Environment = configData.Environment, ProjectId = configData.ProjectId };
            Initialize(options);
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
        public static void Initialize(InitOptions options = null)
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
        /// Initializes all the Api clients for the SDK to function
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
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
            Matches = new SPMatchesApiClient(Config);
            Progression = new SPProgressionApiClient(Config);
            Rewards = new SPRewardsApiClient(Config);
            Stores = new SPStoreApiClient(Config);
            Tasks = new SPTasksApiClient(Config);
            User = new SPUserApiClient(Config);
            Wallet = new SPWalletApiClient(Config);
            
            IsInitialized = true;
        }

        /// <summary>
        /// Completely reset Specter
        /// </summary>
        public static void Dispose()
        {
            App = null;
            Auth = null;
            Events = null;
            Inventory = null;
            Matches = null;
            Progression = null;
            Rewards = null;
            Stores = null;
            Tasks = null;
            User = null;
            Wallet = null;
            
            Config = null;
            IsInitialized = false;
        }
    }
}
