using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPTaskResource : ISpecterResource, ISpecterRewardable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        
        public SPEvent Event { get; set; }
        
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };

        public SPRewards LinkedRewardDetails { get; set; }
        public bool HasLinkedRewards => LinkedRewardDetails?.All is { Count: > 0 };
        
        public SPTaskResource() { }
        public SPTaskResource(SPTaskResourceData taskResource)
        {
            Uuid = taskResource.uuid;
            Id = taskResource.id;
            Name = taskResource.name;
            Description = taskResource.description;
            IconUrl = taskResource.iconUrl;
        }
    }

    public class SPTaskGroupResource : ISpecterResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public SPTaskGroupType TaskGroupType { get; set; }
        
        public SPTaskGroupResource() { }
        public SPTaskGroupResource(SPTaskGroupResourceData taskGroupResource)
        {
            Uuid = taskGroupResource.uuid;
            Id = taskGroupResource.id;
            Name = taskGroupResource.name;
            Description = taskGroupResource.description;
            IconUrl = taskGroupResource.iconUrl;
            TaskGroupType = taskGroupResource.taskGroupType;
        }
    }
    
    public class SPTask : ISpecterResource, ISpecterMasterObject, ISpecterLiveOpsEntity, ISpecterUnlockable, ISpecterRewardable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPSchedule Schedule { get; set; }
        
        public int? SortingOrder { get; set; }
        public SPTaskGroupResource TaskGroupDetails { get; set; }
        public SPEvent Event { get; set; }
        public SPBusinessLogic BusinessLogic { get; set; }
        public List<SPRuleParam> Parameters { get; set; }
        
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        
        public SPRewards LinkedRewardDetails { get; set; }
        public bool HasLinkedRewards => LinkedRewardDetails?.All is { Count: > 0 };
        
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        public SPTask() { }
        public SPTask(SPTaskData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Schedule = data.schedule == null ? null : new SPSchedule(data.schedule);
            
            SortingOrder = data.sortingOrder;
            TaskGroupDetails = data.taskGroupDetails == null ? null : new SPTaskGroupResource(data.taskGroupDetails);
            Event = new SPEvent(data.@event);
            BusinessLogic = data.businessLogic == null ? null : new SPBusinessLogic(data.businessLogic);
            Parameters = data.parameters?.ConvertAll(x => new SPRuleParam(x));
            
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
            LinkedRewardDetails = data.linkedRewardDetails == null ? null : new SPRewards(data.linkedRewardDetails);
            
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);

            Tags = data.tags;
            Meta = data.meta;
        }
    }
    
    public class SPTaskGroup : ISpecterResource, ISpecterMasterObject, ISpecterLiveOpsEntity, ISpecterUnlockable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPSchedule Schedule { get; set; }
        
        public SPTaskGroupType TaskGroupType { get; set; }
        public List<SPTaskResource> Tasks { get; set; }
        
        public SPUnlockConditions UnlockConditions { get; set; }
        public bool IsLocked => UnlockConditions.IsLocked;
        public bool IsLockedByLevel => UnlockConditions.IsLockedByLevel;
        public bool IsLockedByItem => UnlockConditions.IsLockedByItem;
        public bool IsLockedByBundle => UnlockConditions.IsLockedByBundle;
        
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        
        protected SPTaskGroup() { }
        protected SPTaskGroup(ISpecterTaskGroupData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Schedule = data.schedule == null ? null : new SPSchedule(data.schedule);
            
            TaskGroupType = data.taskGroupType;
            Tasks = data.tasks?.ConvertAll(x => new SPTaskResource(x));
            
            UnlockConditions = data.unlockConditions == null ? null : new SPUnlockConditions(data.unlockConditions);
            
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SPMission : SPTaskGroup, ISpecterRewardable
    {
        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        
        public SPRewards LinkedRewardDetails { get; set; }
        public bool HasLinkedRewards => LinkedRewardDetails?.All is { Count: > 0 };
        
        public SPMission() { }
        public SPMission(SPMissionData data) : base(data)
        {
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
            LinkedRewardDetails = data.linkedRewardDetails == null ? null : new SPRewards(data.linkedRewardDetails);
        }
    }

    public class SPStepSeries : SPTaskGroup
    {
        public SPStepSeries() { }
        public SPStepSeries(SPStepSeriesData data) : base(data) { }
    }
}