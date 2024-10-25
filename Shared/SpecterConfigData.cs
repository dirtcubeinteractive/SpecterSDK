using System;
using System.Collections.Generic;
using SpecterSDK.ObjectModels;
using UnityEngine;

namespace SpecterSDK.Shared
{
    public enum SPEnvironment
    {
        Development,
        QualityAssurance,
        Production
    }

    [Flags]
    public enum SPLogLevel
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Debug = 4
    }

    public static class SPDebug
    {
        public static Action<string> Log = Debug.Log;
        public static Action<string, UnityEngine.Object> LogCtx = LogContext;
        public static Action<string> LogWarning = Debug.LogWarning;
        public static Action<string, UnityEngine.Object> LogWarningCtx = LogWarningContext;
        public static Action<string> LogError = Debug.LogError;
        public static Action<string, UnityEngine.Object> LogErrorCtx = LogErrorContext;

        public static void SetLogFlags(SPLogLevel level)
        {
            Log = level.HasFlag(SPLogLevel.Debug) ? Debug.Log : _ => { };
            LogCtx = level.HasFlag(SPLogLevel.Debug) ? LogContext : (_, _) => { };

            LogWarning = level.HasFlag(SPLogLevel.Warning) ? Debug.LogWarning : _ => { };
            LogWarningCtx = level.HasFlag(SPLogLevel.Warning) ? LogWarningContext : (_, _) => { };

            LogError = level.HasFlag(SPLogLevel.Error) ? Debug.LogError : _ => { };
            LogErrorCtx = level.HasFlag(SPLogLevel.Error) ? LogErrorContext : (_, _) => { };
        }

        private static void LogContext(string message, UnityEngine.Object obj)
        {
            Debug.Log($"{GetCtxName(obj)}: {message}", obj);
        }

        private static void LogWarningContext(string message, UnityEngine.Object obj)
        {
            Debug.LogWarning($"{GetCtxName(obj)}: {message}", obj);
        }

        private static void LogErrorContext(string message, UnityEngine.Object obj)
        {
            Debug.LogError($"{GetCtxName(obj)}: {message}", obj);
        }

        private static string GetCtxName(UnityEngine.Object obj)
        {
            return obj == null ? "Logger" : obj.GetType().Name;
        }
    }

    /// <summary>
    /// Provides runtime configuration to the SDK based on the provided SpecterConfigData or manual setup
    /// </summary>
    public class SpecterRuntimeConfig
    {
        private static SpecterApiRateConfig RateConfig { get; set; }
        public static SPAuthContext AuthCredentials = new SPAuthContext();

        public string AccessToken 
        { 
            get => AuthCredentials.AccessToken;
            set => AuthCredentials.AccessToken = value;
        }
        
        public string EntityToken 
        {
            get => AuthCredentials.EntityToken;
            set => AuthCredentials.EntityToken = value;
        }

        public string ApiKey
        {
            get => AuthCredentials.ApiKey;
            set => AuthCredentials.ApiKey = value;
        }

        private readonly SPEnvironment m_Environment;

        private string m_ProjectId;

        private string m_BaseUrl = "https://api.specterapp.xyz";
        public string BaseUrl => m_BaseUrl;

        public SPEnvironment Environment => m_Environment;
        public string ProjectId { get => m_ProjectId; set => m_ProjectId = value; }
        public bool UseDebugCredentials { get; }
        
        public int MaxConcurrentRequests => RateConfig.m_MaxConcurrentRequests;
        public int MaxRetries => RateConfig.m_MaxRetries;
        public int MaxTokens => RateConfig.m_MaxTokens;
        public int TokenRefillMillis => RateConfig.m_TokenRefillIntervalMillis;
        public int BaseRetryDelayMillis => RateConfig.m_RetryBaseDelayMillis;

        public SpecterRuntimeConfig(
            SPEnvironment environment = SPEnvironment.Development, 
            string projectId = "", 
            string apiKey = null, 
            SpecterApiRateConfig rateConfig = default)
        {
            m_Environment = environment;
            m_ProjectId = projectId;

            AuthCredentials.ApiKey = apiKey;
            RateConfig = rateConfig ?? new SpecterApiRateConfig();
        }

        public SpecterRuntimeConfig(SpecterConfigData data) : this(data.Environment, data.ProjectId)
        {
            if (data.Environment != SPEnvironment.Production && data.UseDebugCredentials)
            {
                AuthCredentials = data.DebugAuthContext;
                UseDebugCredentials = true;
            }
            else
                UseDebugCredentials = false;

            AuthCredentials.ApiKey = data.GetApiKey();
            RateConfig = data.RateConfig;
            SPDebug.SetLogFlags(data.LogLevel);
        }

        public void SetInternalConfig()
        {
            var file = Resources.Load<TextAsset>("specter_config");
            if (file == null) 
                return;
            
            var dict = SpecterJson.DeserializeObject<Dictionary<string, object>>(file.text);
            m_BaseUrl = (string)dict["url"];
            Debug.Log($"Specter: Set base url to {m_BaseUrl}");
        }
    }

    [Serializable]
    public class SPApiKeyData
    {
        public SPEnvironment m_Environment;
        public string m_ApiKey;
    }
    
    /// <summary>
    /// Defines configuration data for the Specter SDK.
    /// </summary>
    public class SpecterConfigData: ScriptableObject
    {
        [Tooltip("Auto initialize Specter SDK on launching game or entering Play Mode")]
        [SerializeField] private bool m_AutoInit = true;

        [Tooltip("Log level for SDK. Can be changed at runtime via SPDebug.SetLogFlags method")]
        [SerializeField] private SPLogLevel m_LogLevel = SPLogLevel.Debug;
        
        [Tooltip("Runtime environment for Specter SDK")]
        [SerializeField] private SPEnvironment m_Environment;
        
        [Tooltip("Project ID aka App ID")]
        [SerializeField] private string m_ProjectId;

        [Tooltip("API Keys for each environment")] [SerializeField]
        private List<SPApiKeyData> m_ApiKeys = new List<SPApiKeyData>();

        [Header("DEBUG & TESTING")]
        [Tooltip("Set Use Debug Credentials to true if you need to test Specter APIs (only works in Dev/QualityAssurance Env)")]
        [SerializeField] private bool m_UseDebugCredentials;
        
        [Tooltip("Debug tokens to test the Specter APIs. These can only be used in Development and QualityAssurance Environments")] 
        [SerializeField] private SPAuthContext m_DebugAuthContext;

        [Tooltip("Configurations to handle SDK rate limiting and retries")] 
        [SerializeField] private SpecterApiRateConfig m_ApiRateConfig = new SpecterApiRateConfig();

        public bool AutoInit => m_AutoInit;

        public SPLogLevel LogLevel => m_LogLevel;
        public SPEnvironment Environment => m_Environment;
        public string ProjectId => m_ProjectId;

        public bool UseDebugCredentials => m_UseDebugCredentials;
        public SPAuthContext DebugAuthContext => m_DebugAuthContext;
        
        public SpecterApiRateConfig RateConfig => m_ApiRateConfig;

        public string GetApiKey()
        {
            return m_ApiKeys.Find(x => x.m_Environment == m_Environment)?.m_ApiKey;
        }

        public string GetApiKey(SPEnvironment environment)
        {
            return m_ApiKeys.Find(x => x.m_Environment == environment)?.m_ApiKey;
        }
        
#if UNITY_EDITOR
        public static string DebugAuthContextProp_Id => nameof(m_DebugAuthContext);
        public static string ProjectContextProp_Id => nameof(m_ProjectId);

        public static int PropertyEventKey(string propName) => propName switch
        {
            nameof(m_ProjectId) => 1,
            nameof(m_DebugAuthContext) => 2,
            _ => 0
        };

        public void Reset()
        {
            var values = Enum.GetValues(typeof(SPEnvironment));
            var count = values.Length;
            m_ApiKeys ??= new List<SPApiKeyData>();
            for (int i = 0; i < count; i++)
            {
                m_ApiKeys.Add(new SPApiKeyData()
                {
                    m_Environment = (SPEnvironment)values.GetValue(i),
                    m_ApiKey = ""
                });
            }
        }
#endif
    }

    [Serializable]
    public class SpecterApiRateConfig
    {
        [Range(1, 10)] public int m_MaxRetries = 3;
        public int m_MaxTokens = 25;
        public int m_MaxConcurrentRequests = 3;
        public int m_TokenRefillIntervalMillis = 500;
        public int m_RetryBaseDelayMillis = 1000;
    }
}