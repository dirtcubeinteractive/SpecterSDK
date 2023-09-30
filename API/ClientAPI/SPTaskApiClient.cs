using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.Shared;

namespace SpecterSDK.API.ClientAPI
{
    public class SPTaskApiClient : SpecterApiClientBase
    {
        public override SPAuthType AuthType => SPAuthType.AccessToken;
        
        public SPTaskApiClient(SpecterRuntimeConfig config) : base(config) {}

        public async Task<SPGetTasksResult> GetTasks(SPGetTasksRequest request)
        {
            var result = await PostAsync<SPGetTasksResult, SPTaskResponseDataList>("/v1/client/task/get-tasks", AuthType, request);
            return result;
        }
    }
}