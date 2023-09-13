using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK
{
    using APIClients;
    
    public static class Specter
    {
        #region Path constants & props

        public const string SDK_DIRNAME = "SpecterSDK";
        public const string SHARED_DATA_DIRNAME = "SpecterSharedResources";
        public const string CONFIG_FILENAME = "SpecterConfigData";

        public static string ConfigDataResourcePath => $"{SHARED_DATA_DIRNAME}/{CONFIG_FILENAME}";
        
        #endregion
        
        public static SPUserApi User { get; private set; }
        
        public class SPInitOptions
        {
            public SPEnvironment Environment { get; set; }
        }
        
        public static SpecterRuntimeConfig Config;

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

            var options = new SPInitOptions() { Environment = configData.Environment };
            Initialize(options);
        }
        
        public static void Initialize(SPInitOptions options = null)
        {
            options ??= new SPInitOptions() { Environment = SPEnvironment.Development };
            Config = new SpecterRuntimeConfig(options.Environment);

            User = new SPUserApi(Config);
        }
    }
}
