using UnityEngine;

namespace SpecterSDK.Shared
{
    public enum SPEnvironment
    {
        Development,
        Staging,
        Production
    }

    public class SpecterRuntimeConfig
    {
        public readonly string AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJmNzMzNDY2Yy02ODgzLTQ4YjEtYmEzZS01MGY5OGE0YzUwODgiLCJwcm9qZWN0SWQiOiI3ZWJlOTk4Zi1lM2Q5LTQ2MTYtYTQzMS04NjBlYmRjODhiOWUiLCJpYXQiOjE2OTU5ODUzMTYsImV4cCI6MTY5ODU3NzMxNn0.Ej01Wvz_qyOZu1Xn79T0CzIHpUx0mFqovAS_hW95Pv8";
        public readonly string EntityToken = "b2b93f8acc438b00f772b8bb2906e1a5";
        
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
            ProjectId = projectId;
        }

        public SpecterRuntimeConfig(SpecterConfigData data) : this
        (
            data.Environment,
            data.ProjectId
        ) {}
    }
    
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

        public bool AutoInit => m_AutoInit;
        public SPEnvironment Environment => m_Environment;

        public string ProjectId => m_ProjectId;
    }
}