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
        public string Uuid;
        public string Id;
    }

    public abstract class SpecterResource : SpecterObject
    {
        public string Name;
        public string Description;
        public string IconUrl;
    }

    public class SpecterRewards
    {
        
    }
}