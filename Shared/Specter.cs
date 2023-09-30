using System;
using SpecterSDK.API.ClientAPI;
using SpecterSDK.Shared;
using UnityEditor;
using UnityEngine;

namespace SpecterSDK
{
    public static class Specter
    {
        #region Path constants & props

        public const string SDK_DIRNAME = "SpecterSDK";
        public const string SHARED_DATA_DIRNAME = "SpecterSharedResources";
        public const string CONFIG_FILENAME = "SpecterConfigData";

        public static string ConfigDataResourcePath => $"{SHARED_DATA_DIRNAME}/{CONFIG_FILENAME}";
        
        #endregion
        
        public class SPInitOptions
        {
            public SPEnvironment Environment { get; set; }
            public string ProjectId { get; set; }
        }
        
        public static SpecterRuntimeConfig Config;
        
        public static SPAuthApiClient Auth { get; private set; }
        public static SPUserApiClient User { get; private set; }
        public static SPTasksApiClient Tasks { get; private set; }
        
        public static bool IsInitialized { get; private set; }

        public static SpecterRuntimeConfig LoadConfig()
        {
            var configData = Resources.Load<SpecterConfigData>(ConfigDataResourcePath);
            if (configData != null) 
                return new SpecterRuntimeConfig(configData);
            
            Debug.LogWarning("No Specter config data found. Specter will need to be initialized manually by calling Specter.Initialize()");
            return null;

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void AutoInitialize()
        {
            var configData = Resources.Load<SpecterConfigData>(ConfigDataResourcePath);
            if (configData == null)
            {
                Debug.LogWarning("No Specter config data found. Specter will need to be initialized manually by calling Specter.Initialize()");
                return;
            }

            if (!configData.AutoInit)
                return;

            var options = new SPInitOptions() { Environment = configData.Environment, ProjectId = configData.ProjectId };
            Initialize(options);
        }

        public static void InitializeWithConfig()
        {
            Config = LoadConfig();
            InitializeApi();
        }
        
        public static void Initialize(SPInitOptions options = null)
        {
            options ??= new SPInitOptions() { Environment = SPEnvironment.Development };
            Config = new SpecterRuntimeConfig(options.Environment);
            InitializeApi();
        }

        private static void InitializeApi()
        {
            if (Config == null)
            {
                throw new InvalidOperationException(
                    "Specter Runtime Config must be initialized before initializing the SDK Http clients. " +
                    "Call Specter.Initialize or Specter.InitializeWithConfig or enable Auto Init " +
                    "in SpecterConfigData Scriptable Object");
            }
            
            Auth = new SPAuthApiClient(Config);
            User = new SPUserApiClient(Config);
            Tasks = new SPTasksApiClient(Config);
            
            IsInitialized = true;
        }

        public static void Reload()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                return;
            
            Dispose();
            InitializeWithConfig();
#endif
        }

        public static void Dispose()
        {
            Auth = null;
            User = null;
            Tasks = null;
            Config = null;

            IsInitialized = false;
        }
    }
}
