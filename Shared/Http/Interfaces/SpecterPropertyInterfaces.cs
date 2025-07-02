using System.Collections.Generic;
using SpecterSDK.Shared.Http.Models;

namespace SpecterSDK.Shared.Http.Interfaces
{
    /// <summary>
    /// Interface for request objects that require a project ID.
    /// The SDK uses this interface to automatically set the project ID if it's missing in the request.
    /// </summary>
    public interface IProjectConfigurable
    {
        public string projectId { get; set; }
    }

    public interface ISpecterEventConfigurable
    {
        public Dictionary<string, object> customParams { get; set; }
        public Dictionary<string, object> specterParams { get; set; }

    }
}