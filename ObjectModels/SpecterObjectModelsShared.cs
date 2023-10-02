using System;

namespace SpecterSDK.ObjectModels
{
    using Interfaces;

    /// <summary>
    /// A class that holds credentials needed for API calls requiring authorization.
    /// </summary>
    [Serializable]
    public class SPAuthContext
    {
        // User access token required for most client API calls
        public string AccessToken;
        // Entity token required for certain client API calls such as to refresh an Access Token
        public string EntityToken;
    }
    
    /// <summary>
    /// Base class for all Specter objects. Specter objects are designed to be more
    /// Unity/C# friendly and are mapped from API data models. Specter objects may contain additional
    /// Game specific usage logic, convenience properties, etc.
    /// </summary>
    public abstract class SpecterObject : ISpecterObject
    {
        public string Uuid;
        public string Id;
    }

    /// <summary>
    /// Base class for Specter resources. Specter resources are any in-game entities configured on the Specter dashboard.
    /// This includes entities such as currencies, tasks, items, etc.
    /// </summary>
    public abstract class SpecterResource : SpecterObject
    {
        public string Name;
        public string Description;
        public string IconUrl;
    }
}