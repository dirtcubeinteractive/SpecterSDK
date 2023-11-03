using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTaskBase : SpecterResource 
    {
        public SPRewardClaimType RewardClaim;
        public SpecterReward SpecterReward;
        public bool IsLockedByLevel;

        public SpecterTaskBase() { }
        public SpecterTaskBase(SPTaskResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            RewardClaim = data.rewardClaim;
            IsLockedByLevel = data.isLockedByLevel;
            if (data.rewardDetails != null)
                SpecterReward = new SpecterReward(data.rewardDetails);
        }

    }

    public class SpecterTask : SpecterTaskBase, ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data) : base(data)
        {
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Tags = data.tags;
            Meta = data.meta;
        }
    }

    public class SpecterUserTask : SpecterTaskBase , ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SPTaskStatus Status;
        public SpecterUserTask() { }
        public SpecterUserTask(SPUserTaskResponseData data) : base(data)
        {
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Tags = data.tags;
            Meta = data.meta;
            Status = data.status;
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