using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPUpdateUserProfileRequest : SPApiRequestBaseData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string birthdate { get; set; }
        public string customId { get; set; }
        public bool? isKyc { get; set; }
    }
    
    public class SPUpdateUserProfileResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;
        
        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }
    
    public partial class SPUserApiClient
    {
        public async Task<SPUpdateUserProfileResult> UpdateProfileAsync(SPUpdateUserProfileRequest request, Action<SPUpdateUserProfileResult> onComplete = null)
        {
            var task = PutAsync<SPUpdateUserProfileResult, SPGeneralResponseData>("/v1/client/user/update-profile", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));

            var result = await task;
            return result;
        }
    }
}