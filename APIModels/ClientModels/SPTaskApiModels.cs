using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    #region Api Data Models
    
    public enum SPRewardType
    {
        ProgressionMarker,
        Currency,
        Item,
        Bundle
    }
    
    public sealed class SPRewardClaimType : SPEnum<SPRewardClaimType>
    {
        public static readonly SPRewardClaimType OnClaim = new SPRewardClaimType(0, "on-claim", nameof(OnClaim));
        public static readonly SPRewardClaimType Automatic = new SPRewardClaimType(1, nameof(Automatic).ToLower(), nameof(Automatic));
        
        private SPRewardClaimType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    public sealed class SPTaskType : SPEnum<SPTaskType>
    {
        public static readonly SPTaskType Static = new SPTaskType(0, nameof(Static).ToLower(), nameof(Static));
        public static readonly SPTaskType Daily = new SPTaskType(1, nameof(Daily).ToLower(), nameof(Daily));
        public static readonly SPTaskType Weekly = new SPTaskType(2, nameof(Weekly).ToLower(), nameof(Weekly));
        
        private SPTaskType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    public sealed class SPTaskGroupType : SPEnum<SPTaskGroupType>
    {
        public static readonly SPTaskGroupType Mission = new SPTaskGroupType(0, nameof(Mission).ToLower(), nameof(Mission));
        public static readonly SPTaskGroupType StepSeries = new SPTaskGroupType(1,"step-series", nameof(StepSeries));

        private SPTaskGroupType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    public sealed class SPTaskStatus : SPEnum<SPTaskStatus>
    {
        public static readonly SPTaskStatus Created = new SPTaskStatus(0, "created", nameof(Created));
        public static readonly SPTaskStatus AckReceived = new SPTaskStatus(1, "ack-received ", nameof(AckReceived));

        private SPTaskStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    public sealed class SPTaskGroupStatus : SPEnum<SPTaskGroupStatus>
    {
        public static readonly SPTaskGroupStatus Completed = new SPTaskGroupStatus(0, nameof(Completed).ToLower(), nameof(Completed));
        public static readonly SPTaskGroupStatus Pending = new SPTaskGroupStatus(1,nameof(Pending).ToLower(), nameof(Pending));

        private SPTaskGroupStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    // Base for task data in SDK responses
    [Serializable]
    public class SPTaskResponseBaseData : ISpecterApiResponseData , ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRewardClaimType rewardClaim { get; set; }
        public SPRewardDetailsResponseData rewardDetails { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
        
    }

    [Serializable]
    public class SPForceCompleteTaskResponseData : SPTaskResponseBaseData
    {
        public SPTaskGroupDetailsResponseData taskGroupDetails { get; set; }
    }

    [Serializable]
    public class SPTaskResponseData : SPTaskResponseBaseData
    { 
        public bool isLockedByLevel { get; set; }
    }

    [Serializable]
    public class SPUserTaskResponseData : SPTaskResponseBaseData
    {
        public bool isLockedByLevel { get; set; }
        public SPTaskStatus status { get; set; }
    }

    [Serializable]
    public class SPUserTaskResponseDataList : SPResponseDataList<SPUserTaskResponseData> { }
    
    [Serializable]
    public class SPForceCompleteTaskResponseDataList : SPResponseDataList<SPForceCompleteTaskResponseData> { }
    [Serializable]
    public class SPTaskResponseDataList : SPResponseDataList<SPTaskResponseData> { }

    [Serializable]
    public class SPTaskGroupResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public int? stageLength { get; set; }
        public bool stageReset { get; set; }
        public int? stepNumber { get; set; }
        public SPRewardDetailsResponseData rewardDetails { get; set; }
        public SPTaskType taskType { get; set; }
        public SPTaskGroupType taskGroupType { get; set; }
    }

    [Serializable]
    public class SPTaskGroupDetailsResponseData : SPTaskGroupResponseBaseData
    {
        
    }

    [Serializable]
    public class SPTaskGroupResponseData : SPTaskGroupResponseBaseData
    {
        public List<SPTaskResponseData> tasks { get; set; }
    }

    [Serializable]
    public class SPUserTaskGroupResponseData : SPTaskGroupResponseBaseData
    {
        public SPTaskGroupStatus status { get; set; }
        public int completedTasksCount { get; set;}
        public int totalTasksCount { get; set;}
        public List<SPUserTaskResponseData> tasks { get; set; }
    }

    [Serializable]
    public class SPUserTaskGroupResponseDataList : SPResponseDataList<SPUserTaskGroupResponseData> { }

    [Serializable]
    public class SPTaskGroupResponseDataList : SPResponseDataList<SPTaskGroupResponseData> { }

    #endregion
}