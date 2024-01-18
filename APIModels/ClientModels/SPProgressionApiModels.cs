using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.Interfaces;
using SpecterSDK.Shared.SPEnum;

namespace SpecterSDK.APIModels.ClientModels
{
    public sealed class SPProgressionSystemType : SPEnum<SPProgressionSystemType>
    {
        public static readonly SPProgressionSystemType Linear = new SPProgressionSystemType(0, nameof(Linear).ToLower(), nameof(Linear));
        public static readonly SPProgressionSystemType NonLinear = new SPProgressionSystemType(1, "non-linear", nameof(NonLinear));
        private SPProgressionSystemType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }
    
    public sealed class SPRewardGrantScheduleType : SPEnum<SPRewardGrantScheduleType>
    {
        public static readonly SPRewardGrantScheduleType OnCompletion = new SPRewardGrantScheduleType(0,"on-completion", nameof(OnCompletion));
        public static readonly SPRewardGrantScheduleType Custom = new SPRewardGrantScheduleType(1, nameof(Custom).ToLower(), nameof(Custom));
        private SPRewardGrantScheduleType(int id, string name, string displayName = null) : base(id, name, displayName) { }
    }

    [Serializable]
    public class SPProgressionMarkerResponseData : ISpecterMasterData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }

    [Serializable]
    public class SPLevelData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int levelNo { get; set; }
        public int incrementalParameterValue { get; set; }
        public int cumulativeParameterValue { get; set; }
        public SPRewardDetailsResponseData rewardDetails { get; set; }
    }
    
    [Serializable]
    public class SPProgressionResponseBaseData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }
    
    [Serializable]
    public abstract class SPProgressionSystemResponseBaseData : SPProgressionResponseBaseData
    {
        public SPProgressionSystemType type { get; set; }
        public SPProgressionMarkerResponseData progressionMarker { get; set; }
        public SPRewardGrantScheduleType rewardGrantScheduleType { get; set; }
        public string rewardGrantTime { get; set; }
        public string rewardGrantDay { get; set; }
        public List<SPLevelData> levels { get; set; }
       
    }
    
    [Serializable]
    public class SPProgressionSystemResponseData : SPProgressionSystemResponseBaseData, ISpecterMasterData
    {
        public List<string> tags { get; set; }
        public Dictionary<string, string> meta { get; set; }
    }
    

    [Serializable]
    public class SPGetProgressionSystemsResponseData : ISpecterApiResponseData
    {
       public  List<SPProgressionSystemResponseData> progressionSystems { get; set; }
       public int totalCount { get; set; }

    }

    [Serializable]
    public class SPUserProgressResponseBaseData : SPProgressionResponseBaseData
    {
        public float progressionMarkerAmount { get; set; }
    }

    [Serializable]
    public class SPUserProgressResponseData : SPUserProgressResponseBaseData
    {
        public List<SPUserProgressInfoResponseData> progressInfo { get; set; }
    }

    [Serializable]
    public class SPUpdatedUserProgressResponseData : SPUserProgressResponseBaseData
    {
        public List<SPUpdatedUserProgressInfoResponseData> progressInfo { get; set; }
    }

    [Serializable]
    public class SPUserProgressInfoResponseData : ISpecterApiResponseData
    {
        public string progressionSystemId { get; set; }
        public int previousLevelNo { get; set; }
        public int amountToNextLevel { get; set; }
        public int currentLevelNo { get; set; }
    }

    [Serializable]
    public class SPUpdatedUserProgressInfoResponseData : SPUserProgressInfoResponseData
    {
        public bool isLevelUp { get; set; }
    }

    [Serializable]
    public class SPUserProgressDataList : SPResponseDataList<SPUserProgressResponseData> { }
}