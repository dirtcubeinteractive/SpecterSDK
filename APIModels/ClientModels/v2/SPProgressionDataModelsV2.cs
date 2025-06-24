using System;
using System.Collections.Generic;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    [Serializable]
    public class SPProgressionInfoData
    {
        public string progressionSystemId { get; set; }
        public int currentLevelNo { get; set; }
        public int previousLevelNo { get; set; }
        public long amountToNextLevel { get; set; }
        public bool isLevelUp { get; set; }
    }

    [Serializable]
    public class SPMarkerProgressData : ISpecterResourceData
    {
        public string uuid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }

        public long progressionMarkerAmount { get; set; }
        public List<SPProgressionInfoData> progressInfo { get; set; }
    }
}