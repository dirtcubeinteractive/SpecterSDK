using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPRemovePlayerDataRequest: SPApiRequestBase
    {
        public List<string> keysToRemove;
    }
    
    public class SPRemovePlayerDataResult : SpecterApiResultBase<SPRemovePlayerDataResponseData>
    {
        public Dictionary<string, SPPlayerDataResponseData> PlayerDataDict;

        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    public partial class SPUserApiClient
    {
        public void RemovePlayerData(SPRemovePlayerDataRequest request, Action<SPRemovePlayerDataResult> onComplete = null)
        {
            var task = PostAsync<SPRemovePlayerDataResult, SPRemovePlayerDataResponseData>("/v1/client/user/remove-player-data", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));
        }

        public async Task<SPRemovePlayerDataResult> RemovePlayerData(SPRemovePlayerDataRequest request)
        {
            var result = await PostAsync<SPRemovePlayerDataResult, SPRemovePlayerDataResponseData>("/v1/client/user/remove-player-data", AuthType, request);
            return result;
        }
    }
}
