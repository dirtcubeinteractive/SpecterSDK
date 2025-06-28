using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v2.LiveOps
{
    /// <summary>
    /// Represents a request to get the current server time, optionally formatted for a specific timezone.
    /// </summary>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetServerTimeRequest : SPApiRequestBase
    {
        /// <summary>
        /// The timezone to format the server time in. Timezone values can be found in moment js timezone
        /// list <a href="https://gist.github.com/diogocapela/12c6617fc87607d11fd62d2a4f42b02a">here</a>.
        /// </summary>
        public string timezone { get; set; }
    }

    public class SPGetServerTimeResult : SpecterApiResultBase<SPGetServerTimeResponse>
    {
        public SPServerTime ServerTime { get; set; }
        
        // The abbreviated name of the timezone
        public string Abbreviation => ServerTime.Abbreviation;
        
        // An ISO8601-valid string representing the current, local date/time
        public DateTime DateTime => ServerTime.DateTime;
        
        // Current day number of the week, where Sunday is 0
        public int DayOfWeek => ServerTime.DayOfWeek;
        
        // Ordinal date of the current year (values range starting from 1 to 366)
        public int DayOfYear => ServerTime.DayOfYear;
        
        // Flag indicating whether the local time is in daylight savings
        public bool DST => ServerTime.DST;
        
        // An ISO8601-valid string representing the datetime when daylight savings started for this timezone
        public string DstFrom => ServerTime.DstFrom;
        
        // The difference in seconds between the current local time and daylight saving time for the location
        public int DstOffset => ServerTime.DstOffset;
        
        // An ISO8601-valid string representing the datetime when daylight savings will end for this timezone
        public string DstUntil => ServerTime.DstUntil;
        
        // The difference in seconds between the current local time and the time in UTC, excluding any daylight saving difference (see dst_offset)
        public int RawOffset => ServerTime.RawOffset;
        
        // Timezone in Area/Location or Area/Location/Region format
        public string Timezone => ServerTime.Timezone;
        
        // Number of seconds since the Epoch
        public int UnixTime => ServerTime.UnixTime;
        
        // An ISO8601-valid string representing the current date/time in UTC
        public DateTime UtcDatetime => ServerTime.UtcDatetime;
        
        // An ISO8601-valid string representing the offset from UTC
        public string UtcOffset => ServerTime.UtcOffset;
        
        // The current week number
        public int WeekNumber => ServerTime.WeekNumber;
        
        protected override void InitSpecterObjectsInternal()
        {
            ServerTime = Response.data == null ? null : new SPServerTime(Response.data);
        }
    }

    public partial class SPLiveOpsApiClientV2
    {
        public async Task<SPGetServerTimeResult> GetServerTimeAsync(SPGetServerTimeRequest request)
        {
            var result = await PostAsync<SPGetServerTimeResult, SPGetServerTimeResponse>("/v2/client/liveops/get-server-time", AuthType, request);
            return result;
        }
    }
}