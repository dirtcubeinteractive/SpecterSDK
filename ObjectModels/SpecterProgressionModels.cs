using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    public class SpecterProgressionMarker : SpecterResource , ISpecterMasterObject
    {
        public List<string> Tags { get; set; }
        public Dictionary<string, string> Meta { get; set; }

        public SpecterProgressionMarker(SPProgressionMarkerResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Tags = new List<string>();
            Meta = new Dictionary<string, string>();
            Tags = data.tags;
            Meta = data.meta;
        }
    }


    public class SpecterLevel : SpecterObject
    {
        public int ParameterValue;
        public SpecterReward Rewards;
        public string Name;

        public SpecterLevel(SPLevelData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            if (data.rewardDetails != null)
            {
                Rewards = new(data.rewardDetails);
            }
            ParameterValue = data.parameterValue;
        }
    }
    
    public class SpecterProgressionSystem : SpecterResource
    {
        public SPProgressionSystemType Type;
        public SpecterProgressionMarker ProgressionMarker;
        public SPRewardGrantScheduleType RewardGrantScheduleType;
        public string RewardGrantTime ;
        public string RewardGrantDay;
        public List<SpecterLevel> Levels;
        public List<string> Tags;
        public Dictionary<string, string> Meta;

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
            Meta = new Dictionary<string, string>();
            foreach (var level in data.levels)
            {
                Levels.Add(new(level));
            }
        }
    }
    
    public class SpecterUserProgress : SpecterResource
    {
        public float ProgressionMarkerAmount;
        public List<SpecterUserProgressInfo> ProgressInfos; 

        public SpecterUserProgress(SPUserProgressResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            ProgressionMarkerAmount = data.progressionMarkerAmount;
            ProgressInfos = new List<SpecterUserProgressInfo>();
            foreach (var progression in data.progressInfo)
            {
                ProgressInfos.Add(new SpecterUserProgressInfo(progression));
            }
        }
    }

    public class SpecterUpdatedProgress : SpecterResource
    {
        public float ProgressionMarkerAmount;
        public List<SpecterProgressInfo> ProgressInfos;
        public SpecterUpdatedProgress(SPUpdatedProgressResponseData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            ProgressionMarkerAmount = data.progressionMarkerAmount;
            ProgressInfos = new List<SpecterProgressInfo>();
            foreach (var progression in data.progressInfo)
            {
                ProgressInfos.Add(new SpecterProgressInfo(progression));
            }
        }


    }
    public class SpecterProgressInfoBase : SpecterObject
    {
        public int CurrentLevelNo;
        public int AmountToNextLevelNo;
        public int PreviousLevelNo;
        public string ProgressionSystemId;
        public SpecterProgressInfoBase(SPProgressInfoResponseBaseData data)
        {
            CurrentLevelNo = data.currentLevelNo;
            AmountToNextLevelNo = data.amountToNextLevelNo;
            PreviousLevelNo = data.previousLevelNo;
            ProgressionSystemId = data.progressionSystemId;
        }
    }

    public class SpecterUserProgressInfo : SpecterProgressInfoBase
    {
        public SpecterUserProgressInfo(SPUserProgressInfoResponseData data) : base(data)
        {
        }
    }

    public class SpecterProgressInfo : SpecterProgressInfoBase
    {
        public bool IsLevelUp;
        public SpecterProgressInfo(SPProgressInfoResponseData data) : base(data)
        {
            IsLevelUp = data.isLevelUp;
        }
    }

}