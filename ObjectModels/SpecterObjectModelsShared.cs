using System;

namespace SpecterSDK.ObjectModels
{
    using Interfaces;

    [Serializable]
    public class SPAuthContext
    {
        public string AccessToken;
        public string EntityToken;
    }
    
    public abstract class SpecterObject : ISpecterObject
    {
        
    }
}