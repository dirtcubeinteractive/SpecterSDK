using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetRewardsHistoryRequest : SPApiRequestBaseData
    {
        public string status { get; set; }

        public string rewardGrant { get; set; }

        public List<string> taskIds { get; set; }

        public List<string> taskGroupIds { get; set; }

        public List<string> progressionSystemIds { get; set; }

        public List<SPApiRequestEntity> entities { get; set; }
    }

    public class SPGetRewardsHistoryResult : SpecterApiResultBase<SPGetRewardHistoryResponseData>
    {
        protected override void InitSpecterObjectsInternal()
        {
            
        }
    }

    public partial class SPRewardsApiClient
    {
        public async Task<SPGetRewardsHistoryResult> GetRewardHistoryTaskAsync(SPGetRewardsHistoryRequest request)
        {
            var result = await PostAsync<SPGetRewardsHistoryResult, SPGetRewardHistoryResponseData>("/v1/client/rewards/get-history", AuthType, request);
            return result;
        }
    }
}