using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterProgressionMarker : SpecterResource , ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        public SpecterProgressionMarker(SPProgressionMarkerResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = new List<string>();
            Meta = new Dictionary<string, object>();
            Tags = data.tags ?? new List<string>();
            Meta = data.meta ?? new Dictionary<string, object>();
        }
    }

    public class SpecterLevel : SpecterObject
    {
        public int IncrementalParameterValue;
        public int CumulativeParameterValue;
        public SpecterRewardDetails Rewards;
        public string Name;
        public int LevelNo;

        public SpecterLevel(SPLevelData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            LevelNo = data.levelNo;
            if (data.rewardDetails != null)
            {
                Rewards = new(data.rewardDetails);
            }
            
            IncrementalParameterValue = data.incrementalParameterValue;
            CumulativeParameterValue = data.cumulativeParameterValue;
        }
    }
    
    public class SpecterProgressionSystem : SpecterResource, ISpecterMasterObject
    {
        public SPProgressionSystemType Type;
        public SpecterProgressionMarker ProgressionMarker;
        public SPRewardGrantScheduleType RewardGrantScheduleType;
        public string RewardGrantTime ;
        public string RewardGrantDay;
        public List<SpecterLevel> Levels;
        public List<string> Tags { get; set; }
        public Dictionary<string, object> Meta { get; set; }

        public SpecterProgressionSystem(SPProgressionSystemResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = data.tags;
            Meta = data.meta;
            Type = data.type;
            ProgressionMarker = new(data.progressionMarker);
            RewardGrantScheduleType = data.rewardGrantScheduleType;
            RewardGrantTime = data.rewardGrantTime;
            RewardGrantDay = data.rewardGrantDay;
            Levels = new List<SpecterLevel>();
            Tags = new List<string>();
            Meta = new Dictionary<string, object>();
            foreach (var level in data.levels)
            {
                Levels.Add(new(level));
            }
        }
    }

    public class SpecterUserProgressBase : SpecterResource
    {
        public float ProgressionMarkerAmount;
        public SpecterUserProgressBase(SPUserProgressResponseBaseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            ProgressionMarkerAmount = data.progressionMarkerAmount;
        }
    }
    
    public class SpecterUserProgress : SpecterUserProgressBase
    {
        public List<SpecterUserProgressInfo> ProgressInfos; 
        public SpecterUserProgress(SPUserProgressResponseData data) : base(data)
        {
            ProgressInfos = new List<SpecterUserProgressInfo>();
            foreach (var progression in data.progressInfo)
            {
                ProgressInfos.Add(new SpecterUserProgressInfo(progression));
            }
        }
    }

    public class SpecterUserProgressInfo
    {
        public int CurrentLevelNo;
        public int AmountToNextLevel;
        public int PreviousLevelNo;
        public string ProgressionSystemId;
        public SpecterUserProgressInfo(SPUserProgressInfoResponseData data)
        {
            CurrentLevelNo = data.currentLevelNo;
            AmountToNextLevel = data.amountToNextLevel;
            PreviousLevelNo = data.previousLevelNo;
            ProgressionSystemId = data.progressionSystemId;
        }
    }
    public class SpecterUpdatedUserProgressInfo : SpecterUserProgressInfo
    {
        public bool IsLevelUp;
        public SpecterUpdatedUserProgressInfo(SPUpdatedUserProgressInfoResponseData data) : base(data)
        {
            IsLevelUp = data.isLevelUp;
        }
    }

}