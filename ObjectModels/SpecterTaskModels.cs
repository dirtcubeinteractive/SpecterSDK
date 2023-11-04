using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTaskBase : SpecterResource , ISpecterMasterObject
    {
        public SPRewardClaimType RewardClaim;
        public SpecterReward SpecterReward;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterTaskBase() { }
        public SpecterTaskBase(SPTaskResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            RewardClaim = data.rewardClaim;
            Tags = data.tags;
            Meta = data.meta;
            if (data.rewardDetails != null)
                SpecterReward = new SpecterReward(data.rewardDetails);
        }
    }

    public class SpecterForceCompletedTask : SpecterTaskBase
    {
        public SpecterTaskGroupDetails TaskGroupDetails;
        public SpecterForceCompletedTask() 
        {
        }
        public SpecterForceCompletedTask(SPForceCompleteTaskResponseData data) : base(data)
        {
        }
    }

    public class SpecterTask : SpecterTaskBase
    {
        public bool IsLockedByLevel;
       
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data) : base(data)
        {
            IsLockedByLevel = data.isLockedByLevel;
        }
    }

    public class SpecterUserTask : SpecterTaskBase
    {
        public bool IsLockedByLevel;
        public SPTaskStatus Status;
        public SpecterUserTask() { }
        public SpecterUserTask(SPUserTaskResponseData data) : base(data)
        {
            Status = data.status;
            IsLockedByLevel = data.isLockedByLevel;
        }
    }

    public class SpecterTaskCollection : SpecterObjectList<SpecterTask> { }


    public class SpecterTaskGroupBase : SpecterResource
    {
        public int? StageLength;
        public int? StepNumber;
        public bool StageReset;
        public SPTaskType TaskType;
        public SPTaskGroupType TaskGroupType;
        public SpecterReward Rewards;
        public SpecterTaskGroupBase(SPTaskGroupResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            StageLength = data.stageLength;
            StageReset = data.stageReset;
            StepNumber = data.stepNumber;
            TaskType = data.taskType;
            TaskGroupType = data.taskGroupType;
            if (data.rewardDetails != null)
                Rewards = new SpecterReward(data.rewardDetails);
        }
    }

    public class SpecterTaskGroupDetails : SpecterTaskGroupBase
    {
        public SpecterTaskGroupDetails(SPTaskGroupDetailsResponseData data) : base(data)
        {
            
        }
    }
    public class SpecterTaskGroup : SpecterTaskGroupBase
    {
        public List<SpecterTask> Tasks;
        public SpecterTaskGroup(SPTaskGroupResponseData data) : base(data)
        {
            Tasks = new List<SpecterTask>();
            if (data.tasks != null)
            {
                foreach (var taskResponseData in data.tasks)
                {
                    Tasks.Add(new SpecterTask(taskResponseData));
                }
            }
        }
    }

    public class SpecterUserTaskGroup : SpecterTaskGroupBase
    {
        public List<SpecterUserTask> Tasks;
        public SPTaskGroupStatus Status;
        public int CompletedTasksCount;
        public int TotalTasksCount;
        public SpecterUserTaskGroup(SPUserTaskGroupResponseData data) : base(data)
        {
            Tasks = new List<SpecterUserTask>();
            if (data.tasks != null)
            {
                foreach (var taskResponseData in data.tasks)
                {
                    Tasks.Add(new SpecterUserTask(taskResponseData));
                }
            }
        }
    }
}