using System.Collections.Generic;

namespace SpecterSDK.APIModels.Interfaces
{
    /// <summary>
    /// Interface that marks a class as a valid data structure for API responses.
    /// </summary>
    public interface ISpecterApiResponseData { }

    /// <summary>
    /// Interface for data structures that represent master data aka app data in the system.
    /// Master data typically refers to objects configured on the Specter Dashboard
    /// and has tags for categorization and meta information for custom details.
    /// </summary>
    public interface ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
}