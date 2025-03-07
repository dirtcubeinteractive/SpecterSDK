using System;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPPlatformData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string assetBundleUrl { get; set; }
        public string assetBundleVersion { get; set; }
        public string minimumAppVersion { get; set; }
    }

    [Serializable]
    public class SPGenreData
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class SPGameLocationData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    [Serializable]
    public class SPGameCategoryData
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}