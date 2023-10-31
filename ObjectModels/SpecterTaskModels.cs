using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterTask : SpecterResource, ISpecterMasterObject
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
            Tags = data.tags;
            Meta = data.meta;
            
            if(data.rewardDetails != null)
                SpecterReward = new SpecterReward(data.rewardDetails);
        }
    }
    
    public class SpecterTaskCollection : SpecterObjectList<SpecterTask> { }

    public class SpecterTaskGroup : SpecterResource
    {
        public int? StageLength;
        public bool StageReset;
        public SPTaskType TaskType;
        public SPTaskGroupType TaskGroupType;
        public SpecterReward Rewards;
        public List<SpecterTask> Tasks;
        public SpecterTaskGroup(SPTaskGroupResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            StageLength = data.stageLength;
            StageReset = data.stageReset;
            TaskType = data.taskType;
            TaskGroupType = data.taskGroupType;
            
            Tasks = new List<SpecterTask>();
            if (data.tasks != null)
            {
                foreach (var taskResponseData in data.tasks)
                {
                    Tasks.Add(new SpecterTask(taskResponseData));
                }
            }
            
            if (data.rewardDetails != null)
                Rewards = new SpecterReward(data.rewardDetails);
        }
    }
}