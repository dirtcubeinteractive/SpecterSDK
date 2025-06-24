using System.Collections.Generic;
using SpecterSDK.API.v2.Achievements;
using SpecterSDK.API.v2.App;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v2
{
    /// <summary>
    /// Abbreviated information about a task when retrieved in task group objects.
    /// </summary>
    public class SPTaskResource : ISpecterTaskResource, ISpecterRewardable
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
        
        public SPRewardSourceType RewardSource => SPRewardSourceType.Task;
        
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

    /// <summary>
    /// Information about a task that was force completed.
    /// </summary>
    public class SPForceCompletedTaskInfo : ISpecterTaskResource, ISpecterRewardable
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPTaskGroupResource TaskGroupDetails { get; set; }

        public SPRewards RewardDetails { get; set; }
        public bool HasRewards => RewardDetails?.All is { Count: > 0 };
        public SPRewardSourceType RewardSource => SPRewardSourceType.Task;
        
        public SPForceCompletedTaskInfo() { }
        public SPForceCompletedTaskInfo(SPForceCompletedTaskData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            TaskGroupDetails = data.taskGroupDetails == null ? null : new SPTaskGroupResource(data.taskGroupDetails);
            RewardDetails = data.rewardDetails == null ? null : new SPRewards(data.rewardDetails);
        }
    }

    /// <summary>
    /// Minimum information about a task group.
    /// </summary>
    public class SPTaskGroupResource : ISpecterTaskGroupResource
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
    
    /// <summary>
    /// Full information about a task object.
    /// </summary>
    public class SPTask : ISpecterTaskResource, ISpecterMasterObject, ISpecterLiveOpsEntity, ISpecterUnlockable, ISpecterRewardable
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
        
        public SPRewardSourceType RewardSource => SPRewardSourceType.Task;
        
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
    
    /// <summary>
    /// Full information about a task group object.
    /// </summary>
    public class SPTaskGroup : ISpecterTaskGroupResource, ISpecterMasterObject, ISpecterLiveOpsEntity, ISpecterUnlockable
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
        
        public SPRewardSourceType RewardSource => SPRewardSourceType.TaskGroup;
        
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
    
    /// <summary>
    /// Full information about the completion status of a task.
    /// </summary>
    public class SPTaskStatusInfo : ISpecterTaskResource, ISpecterTaskStatusInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public string InstanceId { get; set; }
        public SPTaskStatus Status { get; set; }
        
        public SPTaskGroupResource TaskGroupDetails { get; set; }
        
        public SPTaskStatusInfo() { }
        public SPTaskStatusInfo(SPTaskStatusInfoData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            InstanceId = data.instanceId;
            Status = data.status;
            
            TaskGroupDetails = data.taskGroupDetails == null ? null : new SPTaskGroupResource(data.taskGroupDetails);
        }
    }

    /// <summary>
    /// Full information about the completion status of a task group.
    /// </summary>
    public class SPTaskGroupStatusInfo : ISpecterTaskGroupResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public SPTaskGroupType TaskGroupType { get; set; }
        
        public string InstanceId { get; set; }
        public SPTaskGroupStatus Status { get; set; }
        public List<SPTaskStatusInfoBase> Tasks { get; set; }
        
        public SPTaskGroupStatusInfo() { }
        public SPTaskGroupStatusInfo(SPTaskGroupStatusInfoData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            TaskGroupType = data.taskGroupType;
            
            InstanceId = data.instanceId;
            Status = data.status;
            Tasks = data.tasks?.ConvertAll(x => new SPTaskStatusInfoBase(x));
        }
    }

    /// <summary>
    /// Minimal information about the completion status of a task. Received as the task information within task group statuses.
    /// </summary>
    public class SPTaskStatusInfoBase : ISpecterTaskResource, ISpecterTaskStatusInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public string InstanceId { get; set; }
        public SPTaskStatus Status { get; set; }
        
        public SPTaskStatusInfoBase() { }
        public SPTaskStatusInfoBase(SPTaskStatusInfoBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            InstanceId = data.instanceId;
            Status = data.status;
        }
    }

    // TODO: Make task/achievement resource interface for task related information models
    
    /// <summary>
    /// Information about the progress made within a task with full parameter level progress data.
    /// </summary>
    public class SPTaskProgressInfo : ISpecterTaskResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPEvent Event { get; set; }
        public List<SPParamProgress> Progress { get; set; }
        
        public string TaskGroupId { get; set; }
        
        public SPTaskProgressInfo() { }
        public SPTaskProgressInfo(SPTaskProgressData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Event = new SPEvent(data.@event);
            Progress = data.progress?.ConvertAll(x => new SPParamProgress(x)) ?? new List<SPParamProgress>();
            
            TaskGroupId = data.taskGroupId;
        }
    }
}