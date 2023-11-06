using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTask : SpecterResource , ISpecterMasterObject
    {
        public SPRewardClaimType RewardClaim;
        public SpecterReward SpecterReward;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            RewardClaim = data.rewardClaim;
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Tags = data.tags;
            Meta = data.meta;
            if (data.rewardDetails != null)
                SpecterReward = new SpecterReward(data.rewardDetails);
        }
    }

    public class SpecterForceCompletedTask : SpecterTask
    {
        public SpecterTaskGroupDetails TaskGroupDetails;
        public SpecterForceCompletedTask()
        { }
        public SpecterForceCompletedTask(SPForceCompleteTaskResponseData data) : base(data)
        {
            TaskGroupDetails = new SpecterTaskGroupDetails(data.taskGroupDetails);
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
        public List<SpecterTask> Tasks;
        public SPTaskGroupStatus Status;
        public int CompletedTasksCount;
        public int TotalTasksCount;
        public SpecterUserTaskGroup(SPUserTaskGroupResponseData data) : base(data)
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
}