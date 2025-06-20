using System;
using System.Collections.Generic;
using SpecterSDK.Shared.v2;

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

    [Serializable]
    public class SPRuleParamData : ISpecterRuleParamData
    {
        public string name { get; set; }
        public object targetValue { get; set; }
        public string @operator { get; set; }
        public SPParamDataType dataType { get; set; }
        public SPStatCollectionMode mode { get; set; }
        public SPParameterType type { get; set; }
    }

    [Serializable]
    public class SPParamProgressData : ISpecterRuleParamData
    {
        public string name { get; set; }
        public object targetValue { get; set; }
        public string @operator { get; set; }
        public SPParamDataType dataType { get; set; }
        public SPStatCollectionMode mode { get; set; }
        public SPParameterType type { get; set; }
        
        public long currentValue { get; set; }
    }
    
    [Serializable]
    public class SPRuleData
    {
        /// <summary>
        /// Nested 'AND' rules. Will be null if the rule uses an OR combinator, or if there are no nested rules.
        /// </summary>
        public List<SPRuleData> all { get; set; }
        
        /// <summary>
        /// Nested 'OR' rules. Will be null if the rule uses an AND combinator, or if there are no nested rules.
        /// </summary>
        public List<SPRuleData> any { get; set; }
        
        /// <summary>
        /// The name of the parameter. Will be null if there are nested rules.
        /// </summary>
        public string fact { get; set; }
        
        /// <summary>
        /// The operator to compare the value of the parameter. Will be null if there are nested rules.
        /// </summary>
        public string @operator { get; set; }
        
        /// <summary>
        /// Value of the parameter to compare by for this rule. Will be null if there are nested rules.
        /// </summary>
        public object value { get; set; }
    }

    [Serializable]
    public class SPBusinessLogicData
    {
        public List<SPRuleData> all { get; set; }
        public List<SPRuleData> any { get; set; }
    }
}