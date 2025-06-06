using System;
using SpecterSDK.Shared;

namespace SpecterSDK.APIModels.ClientModels.v1
{
    [Serializable]
    public class SPEvent
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    
    [Serializable]
    public class SPEventParam
    {
        public string name { get; set; }
        public SPParamIncrementalType type { get; set; }
        public SPParamOperatorType @operator { get; set; }
        public SPParamType parameterValue { get; set; }
    }
}