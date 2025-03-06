using System.Collections.Generic;

namespace SpecterSDK.APIModels.ClientModels.v2
{
    public class SPRuleOperators
    {
        public const string GREATER_THAN_INCLUSIVE = "greaterThanInclusive";
        public const string GREATER_THAN = "greaterThan";
        public const string LESS_THAN_INCLUSIVE = "lessThanInclusive";
        public const string LESS_THAN = "lessThan";
        public const string EQUAL = "equal";
        public const string NOT_EQUAL = "notEqual";
    }
    
    public class SPRuleData
    {
        public List<SPRuleData> all { get; set; }
        public List<SPRuleData> any { get; set; }
        public object value { get; set; }
        public string @operator { get; set; }
        public string fact { get; set; }
    }
}