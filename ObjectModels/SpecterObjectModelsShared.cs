using System;
using System.Collections.Generic;
using SpecterSDK.API.v2.App;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.ObjectModels.Interfaces;
using SpecterSDK.ObjectModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels
{
    public class SPUserAuthAccount
    {
        public SPAccountAuthProvider AuthProvider { get; set; }
        public string UserId { get; set; }
        
        public SPUserAuthAccount() { }
        public SPUserAuthAccount(SPUserAuthAccountDataV2 data)
        {
            AuthProvider = data.authProvider;
            UserId = data.userId;
        }
    }
    
    /// <summary>
    /// A class that holds credentials needed for API calls requiring authorization.
    /// </summary>
    [Serializable]
    public class SPAuthContext
    {
        // API Key used specific to configured environment.
        public string ApiKey;
        // User access token required for most client API calls.
        public string AccessToken;
        // Entity token required for certain client API calls such as to refresh an Access Token
        public string EntityToken;
    }

    /// <summary>
    /// Base class for all Specter objects. Specter objects are designed to be more
    /// Unity/C# friendly and are mapped from API data models. Specter objects may contain additional
    /// Game specific usage logic, convenience properties, etc.
    /// </summary>
    public abstract class SpecterObject : ISpecterObject
    {
        public string Uuid;
        public string Id;

        protected SpecterObject() {}
        protected SpecterObject(string uuid, string id)
        {
            Uuid = uuid;
            Id = id;
        }
    }

    /// <summary>
    /// Base class for Specter resources. Specter resources are any in-game entities configured on the Specter dashboard.
    /// This includes entities such as currencies, tasks, items, etc.
    /// </summary>
    public abstract class SpecterResource : SpecterObject
    {
        public string Name;
        public string Description;
        public string IconUrl;

        protected SpecterResource() { }
        protected SpecterResource(ISpecterResourceData data) : base(data.uuid, data.id)
        {
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }

    public class SPServerTime
    {
        public string Abbreviation { get; set; }
        public string ClientIp { get; set; }
        public DateTime DateTime { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfYear { get; set; }
        public bool DST { get; set; }
        public string DstFrom { get; set; }
        public int DstOffset { get; set; }
        public string DstUntil { get; set; }
        public int RawOffset { get; set; }
        public string Timezone { get; set; }
        public int UnixTime { get; set; }
        public DateTime UtcDatetime { get; set; }
        public string UtcOffset { get; set; }
        public int WeekNumber { get; set; }

        public SPServerTime() { }
        public SPServerTime(SPServerTimeData data)
        {
            Abbreviation = data.abbreviation;
            ClientIp = data.clientIp;
            DateTime = data.datetime;
            DayOfWeek = data.dayOfWeek;
            DayOfYear = data.dayOfYear;
            DST = data.dst;
            DstFrom = data.dstFrom;
            DstOffset = data.dstOffset;
            DstUntil = data.dstUntil;
            RawOffset = data.rawOffset;
            Timezone = data.timezone;
            UnixTime = data.unixtime;
            UtcDatetime = data.utcDatetime;
            UtcOffset = data.utcOffset;
            WeekNumber = data.weekNumber;
        }
    }

    public class SPSchedule
    {
        /// <summary>
        /// The start date of the schedule.
        /// </summary>
        public DateTime FirstInstanceStartDate { get; set; }
        
        /// <summary>
        /// The end date of only the first occurence of the schedule.
        /// In a non-recurring schedule, this will be the end date of the entire schedule.
        /// </summary>
        public DateTime? FirstInstanceEndDate { get; set; }
        
        /// <summary>
        /// The unit of time for the length of each occurrence of the schedule.
        /// </summary>
        public SPIntervalUnit IntervalUnit { get; set; }
        
        /// <summary>
        /// The length of each occurrence of the schedule.
        /// </summary>
        public long IntervalLength { get; set; }
        
        /// <summary>
        /// The number of occurrences of the schedule.
        /// </summary>
        public long Occurrences { get; set; }
        
        /// <summary>
        /// Flag indicating whether the schedule is recurring.
        /// </summary>
        public bool IsRecurring { get; set; }
        
        /// <summary>
        /// Information about the current instance of the schedule.
        /// </summary>
        public SPInstanceSchedule CurrentInstanceSchedule { get; set; }
        
        public SPSchedule() { }
        public SPSchedule(SPScheduleData data)
        {
            FirstInstanceStartDate = data.firstInstanceStartDate;
            FirstInstanceEndDate = data.firstInstanceEndDate;
            IntervalUnit = data.intervalUnit;
            IntervalLength = data.intervalLength;
            Occurrences = data.occurrences;
            IsRecurring = data.isRecurring;
            CurrentInstanceSchedule = data.currentInstanceSchedule == null ? null : new SPInstanceSchedule(data.currentInstanceSchedule);
        }
    }

    public class SPInstanceSchedule
    {
        /// <summary>
        /// Unique identifier for the instance.
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// The status of the specified instance. For tasks this can be compared against values found in
        /// <see cref="SPTasksScheduleStatus"/> and for competitions values can be compared against <see cref="SPScheduleStatus"/> values.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// The start date of the instance.
        /// </summary>
        public DateTime InstanceStartDate { get; set; }
        
        /// <summary>
        /// The end date of the instance.
        /// </summary>
        public DateTime? InstanceEndDate { get; set; }
        
        public SPInstanceSchedule() { }
        public SPInstanceSchedule(SPInstanceScheduleData data)
        {
            InstanceId = data.instanceId;
            Status = data.status;
            InstanceStartDate = data.instanceStartDate;
            InstanceEndDate = data.instanceEndDate;
        }
    }
    
    public class SPEvent 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public SPEvent() { }
        public SPEvent(SPEventData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }

    public class SPRuleParam : ISpecterRuleParam
    {
        public string Name { get; set; }
        public object TargetValue { get; set; }
        public string Operator { get; set; }
        public SPParamDataType DataType { get; set; }
        public SPParamEvalMode Mode { get; set; }
        public SPParameterType Type { get; set; }
        
        public SPRuleParam() { }
        public SPRuleParam(SPRuleParamData data)
        {
            Name = data.name;
            TargetValue = data.targetValue;
            Operator = data.@operator;
            DataType = data.dataType;
            Mode = data.mode;
            Type = data.type;
        }
    }
    
    public class SPParamProgress : ISpecterRuleParam
    {
        public string Name { get; set; }
        public object TargetValue { get; set; }
        public string Operator { get; set; }
        public SPParamDataType DataType { get; set; }
        public SPParamEvalMode Mode { get; set; }
        public SPParameterType Type { get; set; }
        
        public object CurrentValue { get; set; }
        
        public SPParamProgress() { }
        public SPParamProgress(SPParamProgressData data)
        {
            Name = data.name;
            TargetValue = data.targetValue;
            Operator = data.@operator;
            DataType = data.dataType;
            Mode = data.mode;
            Type = data.type;
            
            CurrentValue = data.currentValue;
        }
    }

    public class SPRule
    {
        public List<SPRule> All { get; set; }
        public List<SPRule> Any { get; set; }
        public string Fact { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        
        public SPRule() { }
        public SPRule(SPRuleData data)
        {
            All = data.all?.ConvertAll(x => new SPRule(x));
            Any = data.any?.ConvertAll(x => new SPRule(x));
            Fact = data.fact;
            Operator = data.@operator;
            Value = data.value;
        }
    }

    public class SPBusinessLogic
    {
        public List<SPRule> All { get; set; }
        public List<SPRule> Any { get; set; }
        
        public SPBusinessLogic() { }
        public SPBusinessLogic(SPBusinessLogicData data)
        {
            All = data.all?.ConvertAll(x => new SPRule(x));
            Any = data.any?.ConvertAll(x => new SPRule(x));
        }
    }

    public class SPDocumentInfo
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public SPDocumentInfo() { }
        public SPDocumentInfo(SPDocumentData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
        }
    }
}