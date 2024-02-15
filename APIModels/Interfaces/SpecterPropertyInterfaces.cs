using System.Collections.Generic;

namespace SpecterSDK.APIModels.Interfaces
{
    /// <summary>
    /// Interface for request objects that require a project ID.
    /// The SDK uses this interface to automatically set the project ID if it's missing in the request.
    /// </summary>
    public interface IProjectConfigurable
    {
        public string projectId { get; set; }
    }

    /// <summary>
    /// Interface for request objects that can be customized with specific attributes.
    /// Attributes can be any fields needed in the response.
    /// <example>User attributes can be "firstName", "lastName", etc.</example>
    /// </summary>
    public interface IAttributeConfigurable
    {
        public List<string> attributes { get; set; }
    }

    /// <summary>
    /// Interface for request objects that permit additional entities to be fetched from the server.
    /// Entities can be any data structures or objects that the request's response relates to.
    /// <example>An entity for a progression system is items - the item rewards for each level</example>
    /// <seealso cref="SPApiRequestEntity"/>
    /// </summary>
    public interface IEntityConfigurable
    {
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public interface ISpecterEventConfigurable
    {
        public Dictionary<string, object> customParams { get; set; }
        public Dictionary<string, object> specterParams { get; set; }

    }
}