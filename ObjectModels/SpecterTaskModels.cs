using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTaskResource : SpecterResource
    {
        public SpecterTaskResource() { }
        public SpecterTaskResource(SPTaskResourceResponseData data) : base(data) { }
    }

    public class SpecterTaskGroupResource : SpecterTaskResource
    {
        public SPTaskGroupType TaskGroupType;

        public SpecterTaskGroupResource() { }
        public SpecterTaskGroupResource(SPTaskGroupResourceResponseData data) : base(data)
        {
            TaskGroupType = data.taskGroupType;
        }
    }

    public class SpecterTask : SpecterTaskResource , ISpecterMasterObject
    {
        public SPRewardGrantType RewardGrantType;
        public SpecterRewardDetails Rewards;
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data) : base(data)
        {
            RewardGrantType = data.rewardGrant;
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Tags = data.tags;
            Meta = data.meta;
            if (data.rewardDetails != null)
                Rewards = new SpecterRewardDetails(data.rewardDetails);
        }
    }

    public class SpecterTaskStatus : SpecterTaskResource
    {
        public SPTaskStatus Status;
        public SpecterTaskStatus() { }

        public SpecterTaskStatus(SPTaskStatusResponseData data) : base(data)
        {
            Status = data.status;
        }
    }

    public class SpecterForceCompletedTaskInfo : SpecterTaskResource
    {
        public SpecterRewardDetails Rewards;
        public SpecterTaskGroupResource TaskGroupInfo;

        public bool IsInTaskGroup => TaskGroupInfo != null;

        public SpecterForceCompletedTaskInfo(SPForceCompletedTaskResponseData data) : base(data)
        {
            if (data.rewardDetails != null)
                Rewards = new SpecterRewardDetails(data.rewardDetails);

            if (data.taskGroupDetails != null)
                TaskGroupInfo = new SpecterTaskGroupResource(data.taskGroupDetails);
        }

    }

    public class SpecterTaskGroup : SpecterTaskGroupResource
    {
        public int? StageLength;
        public int? StepNumber;
        public bool StageReset;
        public SPTaskType TaskType;
        public List<SpecterTask> Tasks;
        public SpecterRewardDetails Rewards;
        public SpecterTaskGroup(SPTaskGroupResponseData data) : base(data)
        {
            StageLength = data.stageLength;
            StageReset = data.stageReset;
            StepNumber = data.stepNumber;
            TaskType = data.taskType;
            
            if (data.rewardDetails != null)
                Rewards = new SpecterRewardDetails(data.rewardDetails);

            Tasks = new List<SpecterTask>();
            foreach (var taskData in data.tasks)
            {
                Tasks.Add(new SpecterTask(taskData));
            }
        }
    }

    public class SpecterTaskGroupStatus : SpecterTaskGroupResource
    {
        public List<SpecterTaskStatus> Tasks;
        public SPTaskGroupStatus Status;
        
        public int PendingTasksCount { get; set; }
        public int CompletedTasksCount { get; set; }
        public int TaskRewardsClaimedCount { get; set; }
        public int TotalTasksCount { get; private set; }
        
        public SpecterTaskGroupStatus(SPTaskGroupStatusResponseData data) : base(data)
        {
            PendingTasksCount = 0;
            CompletedTasksCount = 0;
            TaskRewardsClaimedCount = 0;

            Tasks = new List<SpecterTaskStatus>();
            if (data.tasks != null)
            {
                foreach (var taskResponseData in data.tasks)
                {
                    Tasks.Add(new SpecterTaskStatus(taskResponseData));

                    if (taskResponseData.status == SPTaskStatus.Pending)
                        PendingTasksCount++;
                    else if (taskResponseData.status == SPTaskStatus.Completed)
                        CompletedTasksCount++;
                    else if (taskResponseData.status == SPTaskStatus.RewardClaimed)
                        TaskRewardsClaimedCount++;
                }
            }

            TotalTasksCount = Tasks.Count;
        }
    }
}