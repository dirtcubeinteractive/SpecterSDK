using System.Collections.Generic;

namespace SpecterSDK.APIModels.Interfaces
{
    public interface ISpecterApiResponseData { }

    public interface ISpecterCustomConfiguredData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
}