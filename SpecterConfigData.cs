using UnityEngine;

namespace SpecterSDK
{
    using APIClients;
    
    public class SpecterRuntimeConfig
    {
        public readonly string AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJkM2NlNGY3OS02NzRlLTQ1MDktODdhMC0yMzM0NDFmNjAxNTkiLCJwcm9qZWN0SWQiOiIyNjI3Yjk3ZC0zNTEyLTRhZjQtODhmNS1jZTMyZTRlMDM5ODAiLCJpYXQiOjE2OTQxMTQxMzQsImV4cCI6MTY5NjcwNjEzNH0.ENagA0nHVp9cRgAv5r2WsCXaGOV5_8p5twMjw38mCTM";
        public readonly string EntityToken = "43f0e1380fd1d3850b7b18f201494272";
        
        private readonly SPEnvironment m_Environment;
        private readonly string m_DevUrl;
        private readonly string m_StagingUrl;
        private readonly string m_ProductionUrl;

        public string BaseUrl => m_Environment switch
        {
            SPEnvironment.Development => m_DevUrl,
            SPEnvironment.Staging => m_StagingUrl,
            _ => m_ProductionUrl
        };

        public SPEnvironment Environment => m_Environment;

        public SpecterRuntimeConfig(SPEnvironment environment = SPEnvironment.Development)
        {
            m_DevUrl = "https://dev.specterapp.xyz";
            m_StagingUrl = "https://dev.specterapp.xyz";
            m_ProductionUrl ="https://dev.specterapp.xyz";

            m_Environment = environment;
        }
        
        public SpecterRuntimeConfig(SpecterConfigData data) : this()
        {
            m_Environment = data.Environment;
        }
    }
    
    public class SpecterConfigData: ScriptableObject
    {
        [SerializeField] private bool m_AutoInit = true;
        [SerializeField] private SPEnvironment m_Environment;

        public bool AutoInit => m_AutoInit;
        public SPEnvironment Environment => m_Environment;
    }
}