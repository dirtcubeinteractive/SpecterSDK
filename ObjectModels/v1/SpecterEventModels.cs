using System;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.Shared;

namespace SpecterSDK.ObjectModels.v1
{
    [Serializable]
    public class SpecterEvent
    {
        public string Id;
        public string Name;

        public SpecterEvent() {}
        public SpecterEvent(SPEventData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }
    
    [Serializable]
    public class SpecterEventParam
    {
        public string Name;
        public SPParamIncrementalType Type;
        public SPParamOperatorType Operator;
        public SPParamType ParameterValue;

        public SpecterEventParam() {}
        public SpecterEventParam(SPEventParam data)
        {
            Name = data.name;
            Type = data.type;
            Operator = data.@operator;
            ParameterValue = data.parameterValue;
        }
    }
}