using System.Collections.Generic;

namespace SpecterSDK.APIModels.Interfaces
{
    public interface IProjectConfigurable
    {
        public string projectId { get; set; }
    }

    public interface IAttributeConfigurable
    {
        public List<string> attributes { get; set; }
    }

    public interface IEntityConfigurable
    {
        public List<SPApiRequestEntity> entities { get; set; }
    }
}