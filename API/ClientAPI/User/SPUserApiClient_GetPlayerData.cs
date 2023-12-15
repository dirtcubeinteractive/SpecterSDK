using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetPlayerDataRequest : SPApiRequestBase
    {
        public List<string> keys;
        public string userId;
    }
    
    public class SPGetPlayerDataResult: SpecterApiResultBase<SPGetPlayerDataResponseData>
    {
        public Dictionary<string, SPPlayerDataResponseData> PlayerDataDict;

        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    public partial class SPUserApiClient
    {
        public void GetPlayerData(SPGetPlayerDataRequest request, Action<SPGetPlayerDataResult> onComplete = null)
        {
            var task = PostAsync<SPGetPlayerDataResult, SPGetPlayerDataResponseData>("/v1/client/user/get-player-data", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));
        }

        public async Task<SPGetPlayerDataResult> GetPlayerData(SPGetPlayerDataRequest request)
        {
            var result = await PostAsync<SPGetPlayerDataResult, SPGetPlayerDataResponseData>("/v1/client/user/get-player-data", AuthType, request);
            return result;
        }
    }
}
