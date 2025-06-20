using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPProgressionMarkerResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPProgressionMarkerResource() { }
        public SPProgressionMarkerResource(SPProgressionMarkerResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    
    public class SPProgressionSystemResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPProgressionSystemResource() { }
        public SPProgressionSystemResource(SPProgressionSystemResourceData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }
    
    public class SPProgressionMarker : ISpecterResource, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public List<SPProgressionSystemResource> ProgressionSystems { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPProgressionMarker() { }
        public SPProgressionMarker(SPProgressionMarkerData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            ProgressionSystems = data.progressionSystems?.ConvertAll(x => new SPProgressionSystemResource(x));
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }
    
    public class SPProgressionSystem : ISpecterResource, ISpecterMasterObject
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPProgressionMarkerResource ProgressionMarker { get; set; }
        public List<SPProgressionLevel> Levels { get; set; }
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPProgressionSystem() { }
        public SPProgressionSystem(SPProgressionSystemData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            ProgressionMarker = data.progressionMarker == null ? null : new SPProgressionMarkerResource(data.progressionMarker);
            Levels = data.levels?.ConvertAll(x => new SPProgressionLevel(x));
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPProgressionLevel : ISpecterRewardable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public int LevelNo { get; set; }
        public long IncrementalParameterValue { get; set; }
        public long CumulativeParameterValue { get; set; }
        
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        
        public SPRewardSourceType RewardSource => SPRewardSourceType.Level;
        
        public SPProgressionLevel() { }
        public SPProgressionLevel(SPProgressionLevelData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            LevelNo = data.levelNo;
            IncrementalParameterValue = data.incrementalParameterValue;
            CumulativeParameterValue = data.cumulativeParameterValue;
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
        }
    }
}