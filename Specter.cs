using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecterSDK
{
    public static class Specter
    {
        public class SPInitOptions
        {
            public SPEnvironment Environment { get; set; }
        }
        
        public static SpecterRuntimeConfig Config;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void AutoInitialize()
        {
            var configData = Resources.Load<SpecterConfigData>(SpecterRuntimeConfig.DATA_FILE_PATH);
            if (!configData.AutoInit)
                return;

            var options = new SPInitOptions() { Environment = configData.Environment };
            Initialize(options);
        }
        
        public static void Initialize(SPInitOptions options = null)
        {
            options ??= new SPInitOptions() { Environment = SPEnvironment.Development };
            Config = new SpecterRuntimeConfig(options.Environment);
        }
    }
}
