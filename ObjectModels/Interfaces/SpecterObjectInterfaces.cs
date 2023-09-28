using System.Collections.Generic;

namespace SpecterSDK.ObjectModels.Interfaces
{
    public interface ISpecterObject { }

    public interface ISpecterCustomConfiguredObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
    }
}