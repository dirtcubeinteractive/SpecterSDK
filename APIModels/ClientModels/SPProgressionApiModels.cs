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
    public class SPGrantProgressResponseData : SPProgressionResponseBaseData
    {
       public int progressionMarkerAmount { get; set; }
       public int currentLevelNo { get; set; }
       public int previousLevelNo { get; set; }
    }

    [Serializable]
    public class SPLevelData : ISpecterApiResponseData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int parameterValue { get; set; }
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
    public class SPProgressionSystemDataList : SPResponseDataList<SPProgressionSystemResponseData> { }
    
    [Serializable]
    public class SPUserProgressResponseData : SPProgressionResponseBaseData
    {
        public float progressionMarkerAmount { get; set; }
        public List<SPProgressInfoResponseData> progressInfo { get; set; }
    }

    [Serializable]
    public class SPProgressInfoResponseData : ISpecterApiResponseData
    {
        public string progressionSystemId { get; set; }
        public int previousLevelNo { get; set; }
        public int amountToNextLevelNo { get; set; }
        public int currentLevelNo { get; set; }
    }

    [Serializable]
    public class SPUserProgressDataList : SPResponseDataList<SPUserProgressResponseData> { }
}