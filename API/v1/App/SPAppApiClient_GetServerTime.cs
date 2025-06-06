using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.App
{
    [Serializable]
    public class SPGetServerTimeRequest : SPApiRequestBase
    {
        /// <summary>
        /// The timezone for which to retrieve current server time.
        /// See <a href="https://worldtimeapi.org/timezones">OpenAPI Spec</a> for available time zones.
        /// </summary>
        public string timezone { get; set; }
    }

    public class SPGetServerTimeResult : SpecterApiResultBase<SPGetServerTimeResponseData>
    {
        public SpecterServerTime ServerTime;
        
        // The abbreviated name of the timezone
        public string Abbreviation => ServerTime.Abbreviation;
        
        // IP address of the client
        public string ClientIp => ServerTime.ClientIp;
        
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
            ServerTime = new SpecterServerTime(Response.data);
        }
    }

    public partial class SPAppApiClient
    {
        /// <summary>
        /// Gets the server time from the API.
        /// </summary>
        /// <returns>
        /// The task object representing the asynchronous operation. The result contains the server time.
        /// </returns>
        public async Task<SPGetServerTimeResult> GetServerTimeAsync()
        {
            var result = await PostAsync<SPGetServerTimeResult, SPGetServerTimeResponseData>("/v1/client/app/get-server-time", AuthType, null);
            return result;
        }
    }
}
