using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.ObjectModels;

namespace SpecterSDK.API.ClientAPI.Rewards
{
    [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetRewardsHistoryRequest : SPApiRequestBase
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
        public List<SpecterRewardHistory> Items;
        public List<SpecterRewardHistory> Bundles;
        public List<SpecterCurrencyRewardHistory> Currencies;
        public List<SpecterRewardHistory> ProgressionMarkers;

        protected override void InitSpecterObjectsInternal()
        {
            Items = new List<SpecterRewardHistory>();
            Bundles = new List<SpecterRewardHistory>();
            Currencies = new List<SpecterCurrencyRewardHistory>();
            ProgressionMarkers = new List<SpecterRewardHistory>();
            foreach (var item in Response.data.items)
            {
                Items.Add(new SpecterRewardHistory(item));
            }
            foreach (var bundle in Response.data.bundles)
            {
                Bundles.Add(new SpecterRewardHistory(bundle));
            }
            foreach (var currency in Response.data.currencies)
            {
                Currencies.Add(new SpecterCurrencyRewardHistory(currency));
            }
            foreach (var progress in Response.data.progressionMarkers)
            {
                ProgressionMarkers.Add(new SpecterRewardHistory(progress));
            }
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