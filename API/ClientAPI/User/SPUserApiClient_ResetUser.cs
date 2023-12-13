using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.Interfaces;

namespace SpecterSDK.API.ClientAPI.User
{
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPResetUserRequest : SPApiRequestBase
    {
        public List<string> attributes { get; set; }
        public List<SPResetUserRequestEntity> entities { get; set; }
    }

    [Serializable]
    public class SPResetUserRequestEntity : SPApiRequestEntity    
    {  
        public List<string> ids { get; set; }
    }

    public sealed class ResetUserResponseData : List<string>, ISpecterApiResponseData { }

    [Serializable]
    public class SPResetUserResult : SpecterApiResultBase<ResetUserResponseData>    
    {
        public List<string> Object;
        protected override void InitSpecterObjectsInternal()
        {
            Object = Response.data;
        }
    }

    public partial class SPUserApiClient
    {
        public async Task<SPResetUserResult> ResetUserAsync(SPResetUserRequest request)
        {
            var result = await PostAsync<SPResetUserResult, ResetUserResponseData>("/v1/client/user/reset", AuthType, request);
            return result;
        }
    }
}