using System.Collections.Generic;
using SpecterSDK.API.ClientAPI.v2.App;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPMatchResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        public SPMatchResource() { }
        public SPMatchResource(SPMatchResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    
    public class SPMatch : ISpecterResource, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPMatch() { }
        public SPMatch(SPMatchData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }
}