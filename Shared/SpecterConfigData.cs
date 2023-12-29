using SpecterSDK.ObjectModels;
using UnityEngine;

namespace SpecterSDK.Shared
{
    public enum SPEnvironment
    {
        Development,
        Staging,
        Production
    }

    /// <summary>
    /// Provides runtime configuration to the SDK based on the provided SpecterConfigData or manual setup
    /// </summary>
    public class SpecterRuntimeConfig
    {
        public static SPAuthContext AuthCredentials;

        public string AccessToken 
        { 
            get => AuthCredentials?.AccessToken;
            set
            {
                AuthCredentials ??= new SPAuthContext();
                AuthCredentials.AccessToken = value;
            }
        }
        
        public string EntityToken 
        {
            get => AuthCredentials?.EntityToken;
            set
            {
                AuthCredentials ??= new SPAuthContext();
                AuthCredentials.EntityToken = value;
            }
        }

        private readonly SPEnvironment m_Environment;
        private readonly string m_DevUrl;
        private readonly string m_StagingUrl;
        private readonly string m_ProductionUrl;

        private string m_ProjectId;

        public string BaseUrl => m_Environment switch
        {
            SPEnvironment.Development => m_DevUrl,
            SPEnvironment.Staging => m_StagingUrl,
            _ => m_ProductionUrl
        };

        public SPEnvironment Environment => m_Environment;
        public string ProjectId { get => m_ProjectId; set => m_ProjectId = value; }
        public bool UseDebugCredentials { get; }

        public SpecterRuntimeConfig
        (
            SPEnvironment environment = SPEnvironment.Development,
            string projectId = ""
        )
        {
            m_DevUrl = "https://dev.specterapp.xyz";
            m_StagingUrl = "https://dev.specterapp.xyz";
            m_ProductionUrl ="https://dev.specterapp.xyz";

            m_Environment = environment;
            m_ProjectId = projectId;
        }

        public SpecterRuntimeConfig(SpecterConfigData data) : this
        (
            data.Environment,
            data.ProjectId
        )
        {
            if (data.Environment != SPEnvironment.Production && data.UseDebugCredentials)
            {
                AuthCredentials = data.DebugAuthContext;
                UseDebugCredentials = true;
            }
            else
                UseDebugCredentials = false;
        }
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

        [Tooltip("Identifier for your organization on the Specter console")]
        [SerializeField] private string m_OrganizationId;
        
        [Tooltip("Project ID aka App ID")]
        [SerializeField] private string m_ProjectId;

        [Header("DEBUG & TESTING")]
        [Tooltip("Set Use Debug Credentials to true if you need to test Specter APIs (only works in Dev/Staging Env)")]
        [SerializeField] private bool m_UseDebugCredentials;
        
        [Tooltip("Debug tokens to test the Specter APIs. These can only be used in Development and Staging Environments")] 
        [SerializeField] private SPAuthContext m_DebugAuthContext;

        public bool AutoInit => m_AutoInit;
        public SPEnvironment Environment => m_Environment;
        public string ProjectId => m_ProjectId;

        public bool UseDebugCredentials => m_UseDebugCredentials;
        public SPAuthContext DebugAuthContext => m_DebugAuthContext;
        
#if UNITY_EDITOR
        public static string DebugAuthContextProp_Id => nameof(m_DebugAuthContext);
        public static string ProjectContextProp_Id => nameof(m_ProjectId);

        public static int PropertyEventKey(string propName) => propName switch
        {
            nameof(m_ProjectId) => 1,
            nameof(m_DebugAuthContext) => 2,
            _ => 0
        };
#endif
    }
}