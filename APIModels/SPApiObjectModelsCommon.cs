using System.Collections.Generic;

namespace SpecterSDK.APIModels
{
    [System.Serializable]
    public abstract class SPObject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    
    [System.Serializable]
    public abstract class SPCustomizableObject : SPObject
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
}