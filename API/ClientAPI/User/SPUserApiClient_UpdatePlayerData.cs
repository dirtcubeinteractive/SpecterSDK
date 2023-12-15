using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdatePlayerDataRequest : SPApiRequestBase
    {
        public List<SPPlayerDataUnit> playerData { get; set; }
        public SPPlayerDataEntryPermission permission { get; set; }
    }
    
    public class SPUpdatePlayerDataResult : SpecterApiResultBase<SPUpdatePlayerDataResponseData>
    {
        public Dictionary<string, SPPlayerDataResponseData> PlayerDataDict;
        
        protected override void InitSpecterObjectsInternal()
        {
            PlayerDataDict = Response.data;
        }
    }

    [Serializable]
    public class SPPlayerDataUnit
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public partial class SPUserApiClient
    {
        public void UpdatePlayerData(SPUpdatePlayerDataRequest request, Action<SPUpdatePlayerDataResult> onComplete = null)
        {
            var task = PostAsync<SPUpdatePlayerDataResult, SPUpdatePlayerDataResponseData>("/v1/client/user/update-player-data", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));
        }

        public async Task<SPUpdatePlayerDataResult> UpdatePlayerData(SPUpdatePlayerDataRequest request)
        {
            var result = await PostAsync<SPUpdatePlayerDataResult, SPUpdatePlayerDataResponseData>("/v1/client/user/update-player-data", AuthType, request);
            return result;
        }
    }
}