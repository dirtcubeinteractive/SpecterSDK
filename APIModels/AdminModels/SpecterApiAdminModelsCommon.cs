using System;
using Newtonsoft.Json;

namespace SpecterSDK.APIModels.AdminModels
{
    [Serializable]
    public class SPProgressionAccessControlConfig
    {
        [JsonRequired]
        public string levelSystemId;
        [JsonRequired]
        public int level;
        [JsonIgnore] 
        public int selectedSystemIndex;
    }

    public class SPMetaConfig
    {
        public string key;
        public string value;
    }
}