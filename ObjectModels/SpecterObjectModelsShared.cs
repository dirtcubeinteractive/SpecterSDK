using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels
{
    using Interfaces;

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
}