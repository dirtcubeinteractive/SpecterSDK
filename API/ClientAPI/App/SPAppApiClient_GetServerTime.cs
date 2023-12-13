using System;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.App
{

    public class SPGetServerTimeRequest : SPApiRequestBase
    {

    }

    public class SPGetServerTimeResult : SpecterApiResultBase<SPGetServerTimeResponseData>
    {
        public string Abbreviation;
        public string ClientIp;
        public DateTime DateTime;
        public int DayOfWeek;
        public int DayOfYear;
        public bool dst;
        public string dstFrom;
        public int dstOffset;
        public string dstUntil;
        public int rawOffset;
        public string timezone;
        public int UnixTime;
        public DateTime utcDatetime;
        public string utcOffset;
        public int weekNumber;

        protected override void InitSpecterObjectsInternal()
        {           
            Abbreviation = Response.data.abbreviation;
            ClientIp = Response.data.clientIp;
            DateTime = Response.data.datetime;
            DayOfWeek = Response.data.dayOfWeek;
            DayOfYear = Response.data.dayOfYear;
            dst = Response.data.dst;
            dstFrom = Response.data.dstFrom;
            dstOffset = Response.data.dstOffset;
            dstUntil = Response.data.dstUntil;
            rawOffset = Response.data.rawOffset;
            timezone = Response.data.timezone;
            UnixTime = Response.data.unixtime;
            utcDatetime = Response.data.utcDatetime;
            utcOffset = Response.data.utcOffset;
            weekNumber = Response.data.weekNumber;
        }
    }

    public partial class SPAppApiClient
    {
        public async Task<SPGetServerTimeResult> GetServerTime()
        {
            var result = await PostAsync<SPGetServerTimeResult, SPGetServerTimeResponseData>("/v1/client/app/get-server-time", AuthType, null);
            return result;
        }
    }
}
