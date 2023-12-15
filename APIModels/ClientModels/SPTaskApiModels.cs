using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared;
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
    public class SPTaskResponseData : ISpecterApiResponseData , ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRewardGrantType rewardGrant { get; set; }
        public SPRewardDetailsResponseData rewardDetails { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    [Serializable]
    public class SPGetTasksResponseData : ISpecterApiResponseData
    {
        public List<SPTaskResponseData> tasks { get; set; }
        public int totalCount { get; set; }
    }


    [Serializable]
    public class SPForceCompleteTaskResponseData : SPTaskResponseData
    {
        public SPTaskGroupDetailsResponseData taskGroupDetails { get; set; }
    }

    [Serializable]
    public class SPUserTaskResponseData : SPTaskResponseData
    {
        public SPTaskStatus status { get; set; }
    }

    [Serializable]
    public class SPUserTaskResponseDataList : SPResponseDataList<SPUserTaskResponseData> { }
    
    [Serializable]
    public class SPForceCompleteTaskResponseDataList : SPResponseDataList<SPForceCompleteTaskResponseData> { }
 
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
    public class SPGetTaskGroupsResponseData : ISpecterApiResponseData
    {
        public List<SPTaskGroupResponseData> taskGroups { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPUserTaskGroupResponseData : SPTaskGroupResponseBaseData
    {
        public SPTaskGroupStatus status { get; set; }
        public List<SPUserTaskResponseData> tasks { get; set; }
    }

    [Serializable]
    public class SPUserTaskGroupResponseDataList : SPResponseDataList<SPUserTaskGroupResponseData> { }

    #endregion
}