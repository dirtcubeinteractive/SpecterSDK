using System;
using System.Collections.Generic;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.ObjectModels
{
    using Interfaces;

    /// <summary>
    /// A class that holds credentials needed for API calls requiring authorization.
    /// </summary>
    [Serializable]
    public class SPAuthContext
    {
        // User access token required for most client API calls
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
        protected SpecterResource(SPResourceResponseData data) : base(data.uuid, data.id)
        {
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
        }
    }

    public enum SpecterResourceLockType
    {
        Progression,
        Item,
        Bundle
    }

    public class SpecterUnlockCondition
    {
        public SpecterResourceLockType LockType;
        public string Id;
        public string Name;
        public int? Value;

        public SpecterUnlockCondition() { }

        public SpecterUnlockCondition(SPUnlockConditionResponseData data)
        {
            LockType = data.unlockProgressionSystem != null
                ? SpecterResourceLockType.Progression
                : (data.unlockItem != null ? SpecterResourceLockType.Item : SpecterResourceLockType.Bundle);
            (Id, Name, Value) = LockType switch
            {
                SpecterResourceLockType.Progression => (data.unlockProgressionSystem!.id, data.unlockProgressionSystem.name, data.lockedLevelNo),
                SpecterResourceLockType.Item => (data.unlockItem!.id, data.unlockItem.name, 1),
                SpecterResourceLockType.Bundle => (data.unlockBundle.id, data.unlockBundle.name, 1),
                _ => throw new NotImplementedException($"Unlock condition of type {LockType} not implemented. Please report Specter SDK bug")
            };
        }
    }

    public class SpecterGamePlatformInfoBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public SpecterGamePlatformInfoBase(SPGamePlatformBaseData data)
        {
            Id = data.id;
            Name = data.name;
        }
    }

    public class SpecterLocation
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CountryCode { get; private set; }

        public SpecterLocation(SPLocationData data)
        {
            Id = data.id;
            Name = data.name;
            CountryCode = data.countryCode;
        }
    }

    public class SpecterServerTime
    {
        public string Abbreviation;
        public string ClientIp;
        public DateTime DateTime;
        public int DayOfWeek;
        public int DayOfYear;
        public bool DST;
        public string DstFrom;
        public int DstOffset;
        public string DstUntil;
        public int RawOffset;
        public string Timezone;
        public int UnixTime;
        public DateTime UtcDatetime;
        public string UtcOffset;
        public int WeekNumber;

        public SpecterServerTime() { }
        public SpecterServerTime(SPGetServerTimeResponseData data)
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