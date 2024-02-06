using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.User
{
    [Serializable]
    public class SPSearchUsersRequest : SPPaginatedApiRequest
    {
        public string search { get; set; }
        public string searchBy { get; set; }
    }

    public class SPSearchUsersResult : SpecterApiResultBase<SPResponseDataList<SPUserProfileResponseBaseData>>
    {
        public List<SpecterUserBase> Users { get; set; }
        
        protected override void InitSpecterObjectsInternal()
        {
            Users = new List<SpecterUserBase>();
            foreach (var user in Response.data)
            {
                Users.Add(new SpecterUserBase(user));
            }
        }
    }

    public partial class SPUserApiClient
    {
        public async Task<SPSearchUsersResult> SearchUsersAsync(SPSearchUsersRequest request)
        {
            var result = await PostAsync<SPSearchUsersResult, SPResponseDataList<SPUserProfileResponseBaseData>>("/v1/client/user/search", AuthType, request);
            return result;
        }
    }
}