using System;

namespace SpecterSDK.APIModels.ClientModels
{
    [Serializable]
    public class SPAppCategoryData
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Class representing genre data for a game
    /// </summary>
    [Serializable]
    public class SPGenreData
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Represents geographical location data. applicable in a any Specter entity that provides info about locational availability
    /// See <a href="https://dirtcube-interactive.gitbook.io/specter-user-manual/app/app-configuration#location">App location configuration</a> for an example use case.
    /// </summary>
    [Serializable]
    public class SPLocationData
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Base class for platform data in the Specter API client models.
    /// </summary>
    [Serializable]
    public class SPPlatformBaseData : ISpecterPlatformData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}