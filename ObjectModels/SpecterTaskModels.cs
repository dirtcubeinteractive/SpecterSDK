using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTaskResource : SpecterResource
    {
        public SPScheduleStates ScheduleStatus;
        
        public SpecterTaskResource() { }

        public SpecterTaskResource(SPTaskResourceResponseData data) : base(data)
        {
            ScheduleStatus = data.scheduleStatus;
        }
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
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int IntervalLength;
        public int? Occurrences;
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        public SpecterTaskGroupResource TaskGroupInfo { get; private set; }
        
        public SpecterTask() { }
        public SpecterTask(SPTaskResponseData data) : base(data)
        {
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences ?? 1;
            
            RewardGrantType = data.rewardGrant;
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
            if (data.rewardDetails != null)
                Rewards = new SpecterRewardDetails(data.rewardDetails);
        }

        public void SetTaskGroupInfo(SPTaskGroupResourceResponseData data)
        {
            TaskGroupInfo = new SpecterTaskGroupResource(data);
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

    public class SpecterTaskGroup : SpecterTaskGroupResource, ISpecterMasterObject
    {
        public int? StageLength;
        public int? StepNumber;
        public bool StageReset;
        
        public DateTime InstanceStartDate;
        public DateTime? InstanceEndDate;
        public SPIntervalUnit IntervalUnit;
        public int IntervalLength;
        public int? Occurrences;
        
        public SPTaskType TaskType;
        public List<SpecterTask> Tasks;
        public SpecterRewardDetails Rewards;
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterTaskGroup(SPTaskGroupResponseData data) : base(data)
        {
            StageLength = data.stageLength;
            StageReset = data.stageReset;
            StepNumber = data.stepNumber;
            
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
            
            TaskType = data.taskType;
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
            
            if (data.rewardDetails != null)
                Rewards = new SpecterRewardDetails(data.rewardDetails);

            Tasks = new List<SpecterTask>();
            foreach (var taskData in data.tasks)
            {
                var task = new SpecterTask(taskData);
                task.SetTaskGroupInfo(data);
                Tasks.Add(task);
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
            Status = data.status;
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

    public class SpecterEventParam
    {
        public string Name;
        public SPParamIncrementalType Type;
        public SPParamOperatorType @Operator;
        public SPParamType ParameterValue;

        public SpecterEventParam(SPEventParam data)
        {
            Name = data.name;
            Type = data.type;
            Operator = data.@operator;
            ParameterValue = data.parameterValue;
        }
    }

    public class SpecterParamProgress : SpecterEventParam
    {
        public object CurrentValue;
        public object TargetValue;

        public SpecterParamProgress(SPParamProgressData data) : base(data)
        {
            CurrentValue = data.currentValue;
            TargetValue = data.targetValue;
        }
    }

    public class SpecterTaskProgress : SpecterResource
    {
        public string EventName;
        public List<SpecterParamProgress> Progresses;

        public SpecterTaskProgress(SPTaskProgressResponseData data) : base(data)
        {
            EventName = data.eventName;
            Progresses = new List<SpecterParamProgress>();
            foreach (var progress in data.progress)
            {
                Progresses.Add(new SpecterParamProgress(progress));
            }
        }
    }

    public class SpecterTaskGroupProgress : SpecterResource
    {
        public SPTaskGroupType TaskGroupType;
        public List<SpecterTaskProgress> Tasks;
        public SpecterTaskGroupProgress(SPTaskGroupProgressResponseData data) : base(data)
        {
            TaskGroupType = data.taskGroupType;
            Tasks = new List<SpecterTaskProgress>();
            foreach (var task in data.tasks)
            {
                Tasks.Add(new SpecterTaskProgress(task));
            }
        }
    }
}