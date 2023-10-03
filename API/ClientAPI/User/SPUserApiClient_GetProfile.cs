using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [System.Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetUserProfileRequest : SPApiRequestBaseData
    {
        public string id { get; set; }
        public List<string> attributes { get; set; }
        public List<SPApiRequestEntity> entities { get; set; }
    }
    
    public class SPGetUserProfileResult : SpecterApiResultBase<SPUserProfileResponseData>
    {
        public SpecterUser User { get; set; }

        protected override void InitSpecterObjectsInternal()
        {
            User = new SpecterUser(Response.data);
        }
    }
    
    public partial class SPUserApiClient
    {
        public async Task<SPGetUserProfileResult> GetProfileAsync(SPGetUserProfileRequest request)
        {
            var defaultAttributes = new List<string>()
            {
                nameof(SPUserResponseBaseData.uuid),
                nameof(SPUserResponseBaseData.id),
                nameof(SPUserResponseBaseData.username),
                nameof(SPUserResponseBaseData.hash)
            };

            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();

            var result = await PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/get-profile", AuthType, request);
            return result;
        }

        public void GetProfile(SPGetUserProfileRequest request, Action<SPGetUserProfileResult> onComplete)
        {
            var defaultAttributes = new List<string>()
            {
                nameof(SPUserResponseBaseData.uuid),
                nameof(SPUserResponseBaseData.id),
                nameof(SPUserResponseBaseData.username),
                nameof(SPUserResponseBaseData.hash)
            };
            
            request.attributes ??= new List<string>();
            request.attributes.AddRange(defaultAttributes);
            request.attributes = request.attributes.Distinct().ToList();
            
            var task = PostAsync<SPGetUserProfileResult, SPUserProfileResponseData>("/v1/client/user/get-profile", AuthType, request);
            task.GetAwaiter().OnCompleted(() => onComplete?.Invoke(task.Result));
        }
    }
}