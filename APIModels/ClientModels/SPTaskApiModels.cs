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

    [Serializable]
    public class SPTaskResourceResponseData : SPResourceResponseData
    {
        public SPScheduleStates scheduleStatus { get; set; }
    }

    [Serializable]
    public class SPTaskGroupResourceResponseData : SPTaskResourceResponseData
    {
        public SPTaskGroupType taskGroupType { get; set; }
    }

    // Base for task data in SDK responses
    [Serializable]
    public class SPTaskResponseData : SPTaskResourceResponseData , ISpecterMasterData
    {
        public SPRewardGrantType rewardGrant { get; set; }
        public SPRewardResourceDetailsResponseData rewardDetails { get; set; }
        public DateTime instanceStartDate { get; set; }
        public DateTime? instanceEndDate { get; set; }
        public SPIntervalUnit intervalUnit { get; set; }
        public int intervalLength { get; set; }
        public int? occurrences { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPGetTasksResponseData : ISpecterApiResponseData
    {
        public List<SPTaskResponseData> tasks { get; set; }
        public int totalCount { get; set; }
    }


    [Serializable]
    public class SPForceCompletedTaskResponseData : SPTaskResourceResponseData
    {
        public SPRewardResourceDetailsResponseData rewardDetails { get; set; }
        public SPTaskGroupResourceResponseData taskGroupDetails { get; set; }
    }

    [Serializable]
    public class SPTaskStatusResponseData : SPTaskResourceResponseData
    {
        public SPTaskStatus status { get; set; }
    }

    [Serializable]
    public class SPTaskStatusResponseDataList : SPResponseDataList<SPTaskStatusResponseData> { }
    
    [Serializable]
    public class SPForceCompleteTaskResponseDataList : SPResponseDataList<SPForceCompletedTaskResponseData> { }

    [Serializable]
    public class SPTaskGroupResponseData : SPTaskGroupResourceResponseData, ISpecterMasterData
    {
        public int? stageLength { get; set; }
        public bool stageReset { get; set; }
        public int? stepNumber { get; set; }
        public SPRewardResourceDetailsResponseData rewardDetails { get; set; }
        public SPTaskType taskType { get; set; }
        public List<SPTaskResponseData> tasks { get; set; }
        public DateTime instanceStartDate { get; set; }
        public DateTime? instanceEndDate { get; set; }
        public SPIntervalUnit intervalUnit { get; set; }
        public int intervalLength { get; set; }
        public int? occurrences { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, object> meta { get; set; }
    }

    [Serializable]
    public class SPGetTaskGroupsResponseData : ISpecterApiResponseData
    {
        public List<SPTaskGroupResponseData> taskGroups { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPTaskGroupStatusResponseData : SPTaskGroupResourceResponseData
    {
        public SPTaskGroupStatus status { get; set; }
        public List<SPTaskStatusResponseData> tasks { get; set; }
    }

    [Serializable]
    public class SPTaskGroupStatusResponseDataList : SPResponseDataList<SPTaskGroupStatusResponseData> { }

    [Serializable]
    public class SPEventParam
    {
        public string name { get; set; }
        public SPParamIncrementalType type { get; set; }
        public SPParamOperatorType @operator { get; set; }
        public SPParamType parameterValue { get; set; }
    }

    [Serializable]
    public class SPParamProgressData : SPEventParam
    {
        public object currentValue { get; set; }
        public object targetValue { get; set; } 
    }


    [Serializable]
    public class SPTaskProgressResponseData : SPResourceResponseData
    {
        public string eventName { get; set; }
        public List<SPParamProgressData> progress { get; set; }
    }

    [Serializable]
    public class SPTaskGroupProgressResponseData : SPResourceResponseData
    {
        public SPTaskGroupType taskGroupType { get; set; }
        public List<SPTaskProgressResponseData> tasks { get; set; }
    }

    [Serializable]
    public class SPGetTaskProgressResponseData : ISpecterApiResponseData
    {
        public List<SPTaskProgressResponseData> taskProgresses { get; set; }
        public int totalCount { get; set; }
    }

    [Serializable]
    public class SPGetTaskGroupProgressResponseData : ISpecterApiResponseData
    {
        public List<SPTaskGroupProgressResponseData> taskGroupProgresses { get; set; }
        public int totalCount { get; set; }
    }

    #endregion
}