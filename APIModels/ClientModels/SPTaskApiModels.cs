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

    public sealed class SPTaskStatus : SPEnum<SPTaskStatus>
    {
        public static readonly SPTaskStatus Created = new SPTaskStatus(0, "created", nameof(Created));
        
        private SPTaskStatus(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    // Reward data in SDK responses
    [Serializable]
    public class SPRewardsResponseData : ISpecterApiResponseData
    {
        private List<SPItemResponseData> items { get; set; }
        private List<SPCurrencyResponseData> currencies { get; set; }
    }

    // Base for task data in SDK responses
    [Serializable]
    public class SPTaskResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public SPRewardsResponseData rewardDetails { get; set; }
    }

    [Serializable]
    public class SPTaskResponseData : SPTaskResponseBaseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

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
        public int? stepNumber { get; set; }
        public int? stageLength { get; set; }
        public bool stageReset { get; set; }
    }

    [Serializable]
    public class SPTaskGroupResponseData : SPTaskGroupResponseBaseData
    {
        
    }

    [Serializable]
    public class SPTaskGroupResponseDataList : SPResponseDataList<SPTaskGroupResponseData> { }

    #endregion

    #region Api Call Models

    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetTasksRequest: SPApiRequestBaseData, IAttributeConfigurable, IEntityConfigurable
    {
        public List<string> taskIds { get; set; }
        public SPTaskStatus status { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetTasksResult : SpecterApiResultBase<SPTaskResponseDataList>
    {
        public List<SpecterTask> Tasks;
        
        protected override void InitSpecterObjectsInternal()
        {
            Tasks = new List<SpecterTask>();
            foreach (var taskData in Response.data)
            {
                Tasks.Add(new SpecterTask(taskData));
            }
        }
    }

    #endregion
}