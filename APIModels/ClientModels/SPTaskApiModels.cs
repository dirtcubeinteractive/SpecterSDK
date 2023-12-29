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

    public class SPTaskResourceResponseData : SPResourceResponseData
    {
    }

    public class SPTaskGroupResourceResponseData : SPTaskResourceResponseData
    {
        public SPTaskGroupType taskGroupType { get; set; }
    }
    
    // Base for task data in SDK responses
    [Serializable]
    public class SPTaskResponseData : SPTaskResourceResponseData , ISpecterMasterData
    {
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
    public class SPForceCompletedTaskResponseData : SPTaskResourceResponseData
    {
        public SPRewardDetailsResponseData rewardDetails { get; set; }
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
        public SPRewardDetailsResponseData rewardDetails { get; set; }
        public SPTaskType taskType { get; set; }
        public List<SPTaskResponseData> tasks { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
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

    #endregion
}