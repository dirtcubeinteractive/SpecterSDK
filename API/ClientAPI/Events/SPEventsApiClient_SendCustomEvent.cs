using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.Events
{
    [Serializable]
    public class SPSendCustomEventsRequest : SPApiRequestBase
    {
        public string eventId { get; set; }
        public Dictionary<string, object> systemParams { get; set; }
        public Dictionary<string, object> customParams { get; set; }
    }

    public class SPSendCustomEventsResult: SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }

    public partial class SPEventsApiClient
    {
        public async Task<SPSendCustomEventsResult> SendCustomEvents(SPSendCustomEventsRequest request)
        {
            var result = await PostAsync<SPSendCustomEventsResult, SPGeneralResponseData>("/v1/client/events/send-custom", AuthType, request); ;
            return result;
        }
    }
}
