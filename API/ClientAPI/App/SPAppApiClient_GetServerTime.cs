using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.App
{
    public class SPGetServerTimeResult : SpecterApiResultBase<SPGetServerTimeResponseData>
    {
        public SpecterServerTime ServerTime;
        protected override void InitSpecterObjectsInternal()
        {
            ServerTime = new SpecterServerTime(Response.data.serverTime);
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
