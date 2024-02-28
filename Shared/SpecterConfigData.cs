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

    /// <summary>
    /// Provides runtime configuration to the SDK based on the provided SpecterConfigData or manual setup
    /// </summary>
    public class SpecterRuntimeConfig
    {
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

        public string BaseUrl => "https://client.staging.specterapp.xyz";

        public SPEnvironment Environment => m_Environment;
        public string ProjectId { get => m_ProjectId; set => m_ProjectId = value; }
        public bool UseDebugCredentials { get; }

        public SpecterRuntimeConfig(SPEnvironment environment = SPEnvironment.Development, string projectId = "", string apiKey = null)
        {
            m_Environment = environment;
            m_ProjectId = projectId;

            AuthCredentials.ApiKey = apiKey;
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

        public bool AutoInit => m_AutoInit;
        public SPEnvironment Environment => m_Environment;
        public string ProjectId => m_ProjectId;

        public bool UseDebugCredentials => m_UseDebugCredentials;
        public SPAuthContext DebugAuthContext => m_DebugAuthContext;

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
}